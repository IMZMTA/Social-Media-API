using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Service.PostService;
using SocialMedia.Domain.Constants;

namespace SocialMedia.Application.Features.Post.Queries.GetAllPosts;

public class GetAllPostsHandler : BaseHandler<GetAllPostsRequestDto, GetAllPostsResponseDto>
{
    private readonly IPostService _postService;

    public GetAllPostsHandler(IPostService postService)
    {
        _postService = postService;
    }

    protected override async Task<ApiResponse<GetAllPostsResponseDto>> ProcessAsync(GetAllPostsRequestDto request, CancellationToken cancellationToken) 
    {

        var posts = await _postService.GetAllPostsAsync(cancellationToken);

        if( posts == null || posts.Count == AppConstants.Zero)
        {
            return new ApiResponse<GetAllPostsResponseDto>()
            {
                Success = true,
                Messages = new List<string> { "No any Posts found" },
                Data = null,
            };
        }

        return new ApiResponse<GetAllPostsResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "Posts fetched successfully" },
            Data = new GetAllPostsResponseDto()
            {
                Posts = posts
            },
        };
    }
}
