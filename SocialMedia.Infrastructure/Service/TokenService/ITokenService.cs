using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Models;

namespace SocialMedia.Infrastructure.Service.TokenService;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken(User user, string jti);

    ClaimsPrincipal GetPrincipalFromToken(string token, bool validateLifetime = true);
    bool IsAccessToken(string token);
    bool IsRefreshToken(string token);
    bool IsValidAccessToken(string token, User user);
    bool IsValidRefreshToken(string token, User user);
    bool IsAccessToken(IEnumerable<Claim> claims);
    bool IsRefreshToken(IEnumerable<Claim> claims);
    bool IsValidAccessToken(IEnumerable<Claim> claims, User user);
    bool IsValidRefreshToken(IEnumerable<Claim> claims, User user);

    string? ExtractTokenFromHeader(HttpContext httpContext);
    string? ExtractRefreshTokenFromCookie(HttpContext httpContext);

    int? GetUserId(ClaimsPrincipal principal);
    string? GetEmail(ClaimsPrincipal principal);
    string? GetUserName(ClaimsPrincipal principal);
    TokenModel GetTokenModel(string token, bool validateLifetime = true);
    TokenModel GetTokenModel(IHttpContextAccessor httpContextAccessor, bool validateLifetime = true);
}
