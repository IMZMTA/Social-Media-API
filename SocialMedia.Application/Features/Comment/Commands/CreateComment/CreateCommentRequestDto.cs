using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.Comment.Commands.CreateComment;

public class CreateCommentRequestDto : IRequest<ApiResponse<CreateCommentResponseDto>>
{
    public string? Content { get; set; }
    public int PostId { get; set; }
}
