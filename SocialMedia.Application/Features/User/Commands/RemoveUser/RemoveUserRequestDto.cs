using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.User.Commands.RemoveUser;

public class RemoveUserRequestDto : IRequest<ApiResponse<RemoveUserResponseDto>>
{
    public int UserId { get; set; }
}
