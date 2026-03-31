using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Models;
using SocialMedia.Infrastructure.Exceptions;
using SocialMedia.Infrastructure.Repository.RefreshTokenRepository;
using SocialMedia.Infrastructure.Repository.UserRepository;
using SocialMedia.Infrastructure.Service.TokenService;

namespace SocialMedia.Infrastructure.Service.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UserModel> RegisterUser(SignUpModel userModel, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = userModel.UserName,
            Email = userModel.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        var createdUser = await _userRepository.CreateUser(user, cancellationToken);

        return new UserModel
        {
            UserId = createdUser.Id,
            UserName = createdUser.UserName,
            Email = createdUser.Email,
            Password = createdUser.Password,
            CreatedAt = createdUser.CreatedAt,
            UpdatedAt = createdUser.UpdatedAt,
            CreatedBy = createdUser.CreatedBy ?? AppConstants.Zero,
            UpdatedBy = createdUser.UpdatedBy ?? AppConstants.Zero
        };
    }

    public async Task<LogInResponseModel> Login(LogInModel loginModel, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailorUsername(loginModel.Identifier, loginModel.Identifier, cancellationToken);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password))
        {
            throw new AuthenticationException("Username or password is incorrect");
        }

        string jti = Guid.NewGuid().ToString();
        string accessToken = _tokenService.GenerateAccessToken(user);
        string refreshToken = _tokenService.GenerateRefreshToken(user, jti);

        var refreshTokenDb = new RefreshToken
        {
            Jti = jti,
            UserId = user.Id,
            User = user,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        await _refreshTokenRepository.AddAsync(refreshTokenDb, cancellationToken);

        _httpContextAccessor.HttpContext!.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = refreshTokenDb.ExpiresAt
        });

        var userModel = new UserModel
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };

        return new LogInResponseModel
        {
            UserId = user.Id,
            AccessToken = accessToken,
            TokenType = AppConstants.AccessToken,
            ExpiresIn = (long)(refreshTokenDb.ExpiresAt - DateTime.UtcNow).TotalSeconds,
            User = userModel
        };


    }

    public async Task<RefreshTokenResponseModel> RefreshToken(HttpContext context, CancellationToken cancellationToken)
    {
        var token = context.Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(token))
            throw new UnauthorizedAccessException("Refresh token missing");

        var claimsPrincipal = _tokenService.GetPrincipalFromToken(token);

        if (!_tokenService.IsRefreshToken(claimsPrincipal.Claims))
        {
            throw new UnauthorizedAccessException("Invalid Refresh Token Type");
        }

        var userId = int.Parse(claimsPrincipal.Claims.First(c => c.Type == "id").Value);
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken) ?? throw new UnauthorizedAccessException("User not found");

        var jti = claimsPrincipal.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value;

        var tokenDb = await _refreshTokenRepository.GetAsync(jti, userId, cancellationToken)
           ?? throw new UnauthorizedAccessException("Invalid refresh token");

        if (tokenDb.IsRevoked || tokenDb.ExpiresAt < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Refresh token expired or revoked");

        tokenDb.IsRevoked = true;
        var newJti = Guid.NewGuid().ToString();
        tokenDb.ReplacedByToken = newJti;

        await _refreshTokenRepository.UpdateAsync(tokenDb, cancellationToken);

        var newRefreshToken = _tokenService.GenerateRefreshToken(user, newJti);
        var newAccessToken = _tokenService.GenerateAccessToken(user);

        var refreshTokenDb = new RefreshToken
        {
            Jti = newJti,
            UserId = user.Id,
            User = user,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        await _refreshTokenRepository.AddAsync(refreshTokenDb, cancellationToken);

        _httpContextAccessor.HttpContext!.Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });

        var userModel = new UserModel
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };

        return new RefreshTokenResponseModel
        {
            UserId = user.Id,
            AccessToken = newAccessToken,
            TokenType = AppConstants.AccessToken,
            ExpiresIn = (long)(refreshTokenDb.ExpiresAt - DateTime.UtcNow).TotalSeconds
        };

    }

    public async Task Logout(HttpContext context, CancellationToken cancellationToken)
    {
        var token = context.Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(token))
            throw new UnauthorizedAccessException("Refresh token missing");

        var principal = _tokenService.GetPrincipalFromToken(token, validateLifetime: false);

        if (!_tokenService.IsRefreshToken(principal.Claims))
            throw new UnauthorizedAccessException("Invalid token type");

        var jti = principal.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
        var userId = int.Parse(principal.Claims.First(c => c.Type == "id").Value);

        var tokenDb = await _refreshTokenRepository.GetAsync(jti, userId, cancellationToken) ?? throw new UnauthorizedAccessException("Refresh token not recognized");

        tokenDb.IsRevoked = true;
        await _refreshTokenRepository.UpdateAsync(tokenDb, cancellationToken);

        context.Response.Cookies.Delete("refreshToken", new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });

        context.Response.Headers.Append("Cache-Control", "no-store, no-cache, must-revalidate, proxy-revalidate");
        context.Response.Headers.Append("Pragma", "no-cache");
        context.Response.Headers.Append("Expires", "0");

        context.User = new System.Security.Claims.ClaimsPrincipal();
    }

    public async Task<int> RemoveByIdAsync(int userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.RemoveByIdAsync(userId, cancellationToken);
        if (user == null)
        {
            return AppConstants.Zero;
        }
        return user.Id;
    }

    public async Task<UserModel?> UpdateByIdAsync(UserModel user, CancellationToken cancellationToken)
    {
        var currUser = await _userRepository.GetByIdAsync(user.UserId, cancellationToken);

        if (currUser == null)
            return null;

        currUser.UserName = user.UserName;
        currUser.Email = user.Email;
        currUser.UpdatedAt = DateTime.UtcNow;

        var updateUser = await _userRepository.UpdateAsync(currUser, cancellationToken);

        if (updateUser == null)
            return null;

        return new UserModel
        {
            UserId = updateUser.Id,
            UserName = updateUser.UserName,
            Email = updateUser.Email
        };
    }

    public async Task<UserModel?> GetById(int userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        
        if(user == null)
        {
            return null;
        }

        return new UserModel
        {
            UserId = user.Id,
            Email = user.Email,
            UserName = user.UserName
        };
    }
    public async Task<List<UserList>> GetAll(CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsers(cancellationToken);
        
        if (users == null || !users.Any())
        { 
            return new List<UserList>();
        }

        return users;
    }
}
