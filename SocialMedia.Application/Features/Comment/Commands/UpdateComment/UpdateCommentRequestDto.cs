using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.Comment.Commands.UpdateComment;

public class UpdateCommentRequestDto : IRequest<ApiResponse<UpdateCommentResponseDto>>
{
    public int CommentId { get; set; }
    public string Content { get; set; } = string.Empty;
}
