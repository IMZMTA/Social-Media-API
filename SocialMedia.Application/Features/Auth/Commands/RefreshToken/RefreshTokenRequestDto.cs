using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenRequestDto : IRequest<ApiResponse<RefreshTokenResponseDto>>
{

}
