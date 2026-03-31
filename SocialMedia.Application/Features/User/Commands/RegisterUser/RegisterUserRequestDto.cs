using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.User.Commands.RegisterUser;

public class RegisterUserRequestDto : IRequest<ApiResponse<RegisterUserResponseDto>>
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
