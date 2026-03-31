using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.Comment.Queries.GetCommentById;

public class GetCommentByIdRequestDto : IRequest<ApiResponse<GetCommentByIdResponseDto>>
{
    public int CommentId { get; set; }
}
