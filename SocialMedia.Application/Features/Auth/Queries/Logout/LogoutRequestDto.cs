using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.Auth.Queries.Logout;

public class LogoutRequestDto : IRequest<ApiResponse<LogoutResponseDto>>
{
    public string Identifier { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
