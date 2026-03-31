using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.Like.Commands.CreateLike;

public class CreateLikeRequestDto : IRequest<ApiResponse<CreateLikeResponseDto>>
{
    public int PostId { get; set; }
}
