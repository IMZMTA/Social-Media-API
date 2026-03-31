using SocialMedia.Domain.Constants;
using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Service.CommentService;

namespace SocialMedia.Application.Features.Comment.Queries.GetAllCommentByPost;

public class GetAllCommentByPostHandler : BaseHandler<GetAllCommentByPostRequestDto, GetAllCommentByPostResponseDto>
{
    private readonly ICommentService _commentService;

    public GetAllCommentByPostHandler(ICommentService commentService)
    {
        _commentService = commentService;
    }

    protected override async Task<ApiResponse<GetAllCommentByPostResponseDto>> ProcessAsync(GetAllCommentByPostRequestDto request, CancellationToken cancellationToken) 
    {

        var comments = await _commentService.GetPostCommentsAsync(request.PostId, cancellationToken);

        if( comments == null || comments.Count == AppConstants.Zero)
        {
            return new ApiResponse<GetAllCommentByPostResponseDto>()
            {
                Success = true,
                Messages = new List<string> { "No any comment found" },
                Data = null,
            };
        }

        return new ApiResponse<GetAllCommentByPostResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "Comments fetched successfully" },
            Data = new GetAllCommentByPostResponseDto()
            {
                Comments = comments
            },
        };
    }
}
