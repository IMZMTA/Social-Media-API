using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.Auth.Queries.LoginUser;

public class LoginUserRequestDto : IRequest<ApiResponse<LoginUserResponseDto>>
{
    public string Identifier { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
