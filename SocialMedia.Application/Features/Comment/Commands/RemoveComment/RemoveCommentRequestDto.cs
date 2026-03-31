using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.Comment.Commands.RemoveComment;

public class RemoveCommentRequestDto : IRequest<ApiResponse<RemoveCommentResponseDto>>
{
    public int CommentId { get; set; }
}
