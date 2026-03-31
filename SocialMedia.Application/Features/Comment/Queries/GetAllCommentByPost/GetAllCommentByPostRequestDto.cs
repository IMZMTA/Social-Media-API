using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.Comment.Queries.GetAllCommentByPost;

public class GetAllCommentByPostRequestDto : IRequest<ApiResponse<GetAllCommentByPostResponseDto>>
{
    public int PostId { get; set; }
}
