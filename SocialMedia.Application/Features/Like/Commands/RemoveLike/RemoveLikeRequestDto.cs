using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.Like.Commands.RemoveLike;

public class RemoveLikeRequestDto : IRequest<ApiResponse<RemoveLikeResponseDto>>
{
    public int LikeId { get; set; }
}
