using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Domain.Config;
using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Models;

namespace SocialMedia.Infrastructure.Service.TokenService;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly SymmetricSecurityKey _key;
    private readonly IHttpContextAccessor _contextAccessor;

    public TokenService(IOptions<AppSettings> settings, IHttpContextAccessor accessor)
    {
        _jwtSettings = settings.Value.Jwt;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        _contextAccessor = accessor;
    }

    #region Token Generation

    public string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new(TokenClaims.Id, user.Id.ToString()),
            new(TokenClaims.Email, user.Email ?? string.Empty),
            new(TokenClaims.UserName, user.UserName ?? string.Empty),
            new(TokenClaims.Type, TokenClaims.AccessToken)
        };

        return BuildToken(user.UserName ?? user.Id.ToString(), Guid.NewGuid().ToString(), claims, DateTime.UtcNow.AddMinutes(10));
    }

    public string GenerateRefreshToken(User user, string jti)
    {
        var claims = new List<Claim>
        {
            new(TokenClaims.Id, user.Id.ToString()),
            new(TokenClaims.Type, TokenClaims.RefreshToken)
        };

        return BuildToken(user.UserName ?? user.Id.ToString(), jti, claims, DateTime.UtcNow.AddDays(7));
    }

    private string BuildToken(string subject, string jti, IEnumerable<Claim> claims, DateTime expires)
    {
        var allClaims = new List<Claim>(claims)
        {
            new(JwtRegisteredClaimNames.Sub, subject),
            new(JwtRegisteredClaimNames.Jti, jti),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: allClaims,
            expires: expires,
            signingCredentials: new SigningCredentials(_key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    #endregion

    #region Validation

    public ClaimsPrincipal GetPrincipalFromToken(string token, bool validateLifetime = true)
    {
        var handler = new JwtSecurityTokenHandler();
        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _key,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            ValidateLifetime = validateLifetime,
            ClockSkew = TimeSpan.Zero
        };

        return handler.ValidateToken(token, parameters, out _);
    }

    private static bool HasTokenType(IEnumerable<Claim> claims, string type) =>
        claims.Any(c => c.Type == TokenClaims.Type && c.Value == type);

    public bool IsAccessToken(IEnumerable<Claim> claims) => HasTokenType(claims, TokenClaims.AccessToken);
    public bool IsRefreshToken(IEnumerable<Claim> claims) => HasTokenType(claims, TokenClaims.RefreshToken);

    public bool IsAccessToken(string token) => IsAccessToken(GetPrincipalFromToken(token).Claims);
    public bool IsRefreshToken(string token) => IsRefreshToken(GetPrincipalFromToken(token).Claims);

    public bool IsValidAccessToken(IEnumerable<Claim> claims, User user) =>
        claims.FirstOrDefault(c => c.Type == TokenClaims.Id)?.Value == user.Id.ToString() && IsAccessToken(claims);

    public bool IsValidRefreshToken(IEnumerable<Claim> claims, User user) =>
        claims.FirstOrDefault(c => c.Type == TokenClaims.Id)?.Value == user.Id.ToString() && IsRefreshToken(claims);

    public bool IsValidAccessToken(string token, User user) =>
        IsValidAccessToken(GetPrincipalFromToken(token).Claims, user);

    public bool IsValidRefreshToken(string token, User user) =>
        IsValidRefreshToken(GetPrincipalFromToken(token).Claims, user);

    #endregion

    #region Extraction

    public string? ExtractTokenFromHeader(HttpContext context)
    {
        var authHeader = context.Request.Headers[AppConstants.Authorization].ToString();
        return authHeader.StartsWith(AppConstants.Bearer) ? authHeader.Substring(AppConstants.Bearer.Length).Trim() : null;
    }

    public string? ExtractRefreshTokenFromCookie(HttpContext context) =>
        context.Request.Cookies.TryGetValue("refreshToken", out var token) ? token : null;

    #endregion

    #region Claims Helpers

    public int? GetUserId(ClaimsPrincipal principal) =>
        int.TryParse(principal.FindFirst(TokenClaims.Id)?.Value, out var id) ? id : null;

    public string? GetEmail(ClaimsPrincipal principal) =>
        principal.FindFirst(TokenClaims.Email)?.Value;

    public string? GetUserName(ClaimsPrincipal principal) =>
        principal.FindFirst(TokenClaims.UserName)?.Value;

    #endregion

    #region TokenModel

    public TokenModel GetTokenModel(string token, bool validateLifetime = true)
    {
        var principal = GetPrincipalFromToken(token, validateLifetime);
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

        return new TokenModel
        {
            UserId = GetUserId(principal),
            Email = GetEmail(principal),
            UserName = GetUserName(principal),
            TokenType = principal.FindFirst(TokenClaims.Type)?.Value ?? string.Empty,
            ExpiresAt = jwt.ValidTo,
            IssuedAt = jwt.IssuedAt
        };
    }

    public TokenModel GetTokenModel(IHttpContextAccessor httpContextAccessor, bool validateLifetime = true)
    {
        var httpContext = httpContextAccessor.HttpContext 
            ?? throw new InvalidOperationException("HttpContext is unavailable");

        var token = ExtractTokenFromHeader(httpContext);
        if (string.IsNullOrEmpty(token)) throw new UnauthorizedAccessException("No token found");
        return GetTokenModel(token);
    }

    #endregion
}
