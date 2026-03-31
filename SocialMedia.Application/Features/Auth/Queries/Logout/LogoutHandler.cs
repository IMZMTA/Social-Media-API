using SocialMedia.Domain.Models;
using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Service.UserService;
using Microsoft.AspNetCore.Http;

namespace SocialMedia.Application.Features.Auth.Queries.Logout;

public class LogoutHandler : BaseHandler<LogoutRequestDto, LogoutResponseDto>
{
    private readonly IUserService userService;
    private readonly IHttpContextAccessor httpContextAccessor;

    public LogoutHandler(IUserService _userService, IHttpContextAccessor _httpContextAccessor)
    {
        userService = _userService;
        httpContextAccessor = _httpContextAccessor;
    }

    protected override async Task<ApiResponse<LogoutResponseDto>> ProcessAsync(LogoutRequestDto request, CancellationToken cancellationToken) 
    {
        var httpContext = httpContextAccessor.HttpContext 
            ?? throw new InvalidOperationException("HttpContext is unavailable");

        await userService.Logout(httpContext, cancellationToken);

        return new ApiResponse<LogoutResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "User Logout successfully" },
            Data = null,
        };
    }
}
