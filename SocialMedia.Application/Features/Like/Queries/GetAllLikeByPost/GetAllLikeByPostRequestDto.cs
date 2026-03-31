using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.Like.Queries.GetAllLikeByPost;

public class GetAllLikeByPostRequestDto : IRequest<ApiResponse<GetAllLikeByPostResponseDto>>
{
    public int PostId { get; set; }
}
