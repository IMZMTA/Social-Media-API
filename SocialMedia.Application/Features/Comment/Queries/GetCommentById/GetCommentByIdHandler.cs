using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Service.CommentService;

namespace SocialMedia.Application.Features.Comment.Queries.GetCommentById;

public class GetCommentByIdHandler : BaseHandler<GetCommentByIdRequestDto, GetCommentByIdResponseDto>
{
    private readonly ICommentService _CommentService;

    public GetCommentByIdHandler(ICommentService CommentService)
    {
        _CommentService = CommentService;
    }

    protected override async Task<ApiResponse<GetCommentByIdResponseDto>> ProcessAsync(GetCommentByIdRequestDto request, CancellationToken cancellationToken)
    {
        var comment = await _CommentService.GetCommentByIdAsync(request.CommentId, cancellationToken);

        if (comment == null )
        {
            return new ApiResponse<GetCommentByIdResponseDto>()
            {
                Success = true,
                Messages = new List<string> { "Comment not found" },
                Data = null,
            };
        }

        return new ApiResponse<GetCommentByIdResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "Comments fetched successfully" },
            Data = new GetCommentByIdResponseDto()
            {
                CommentId = comment.CommentId,
                UserId = comment.UserId,
                Content = comment.Content,
                PostId = comment.PostId,
                CreatedAt = comment.CreatedAt,
                UpdatedAt = comment.UpdatedAt
            },
        };
    }
}
