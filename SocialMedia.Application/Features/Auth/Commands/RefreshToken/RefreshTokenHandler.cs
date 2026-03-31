using Microsoft.AspNetCore.Http;
using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Service.UserService;namespace SocialMedia.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenHandler : BaseHandler<RefreshTokenRequestDto, RefreshTokenResponseDto>
{
    private readonly IUserService userService;
    private readonly IHttpContextAccessor httpContextAccessor;

    public RefreshTokenHandler(IUserService _userService, IHttpContextAccessor _httpContextAccessor)
    {
        userService = _userService;
        httpContextAccessor = _httpContextAccessor;
    }

    protected override async Task<ApiResponse<RefreshTokenResponseDto>> ProcessAsync(RefreshTokenRequestDto request, CancellationToken cancellationToken) 
    {
        var httpContext = httpContextAccessor.HttpContext 
            ?? throw new InvalidOperationException("HttpContext is unavailable");

        var tokenResponseModel = await userService.RefreshToken(httpContext, cancellationToken);

        return new ApiResponse<RefreshTokenResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "User registered successfully" },
            Data = new RefreshTokenResponseDto()
            {
                UserId = tokenResponseModel.UserId, 
                AccessToken = tokenResponseModel.AccessToken,
                TokenType = tokenResponseModel.TokenType,
                ExpiresIn = tokenResponseModel.ExpiresIn,
            },
        };
    }
}
