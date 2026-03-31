using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.Post.Queries.GetPostById;

public class GetPostByIdRequestDto : IRequest<ApiResponse<GetPostByIdResponseDto>>
{
    public int PostId { get; set; }
}
