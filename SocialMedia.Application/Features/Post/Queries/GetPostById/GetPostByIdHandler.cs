using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Service.PostService;

namespace SocialMedia.Application.Features.Post.Queries.GetPostById;

public class GetPostByIdHandler : BaseHandler<GetPostByIdRequestDto, GetPostByIdResponseDto>
{
    private readonly IPostService _postService;

    public GetPostByIdHandler(IPostService postService)
    {
        _postService = postService;
    }

    protected override async Task<ApiResponse<GetPostByIdResponseDto>> ProcessAsync(GetPostByIdRequestDto request, CancellationToken cancellationToken)
    {
        var post = await _postService.GetPostByIdAsync(request.PostId, cancellationToken);

        if (post == null )
        {
            return new ApiResponse<GetPostByIdResponseDto>()
            {
                Success = true,
                Messages = new List<string> { "Post not found" },
                Data = null,
            };
        }

        return new ApiResponse<GetPostByIdResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "Posts fetched successfully" },
            Data = new GetPostByIdResponseDto()
            {
                PostId = post.PostId,
                UserId = post.UserId,
                Content = post.Content,
                LikesCount = post.LikesCount,
                CommentsCount = post.CommentsCount,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt
            },
        };
    }
}
