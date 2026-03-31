using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Service.PostService;
using SocialMedia.Domain.Constants;

namespace SocialMedia.Application.Features.Post.Queries.GetAllPostsByUser;

public class GetAllPostsByUserHandler : BaseHandler<GetAllPostsByUserRequestDto, GetAllPostsByUserResponseDto>
{
    private readonly IPostService _postService;

    public GetAllPostsByUserHandler(IPostService postService)
    {
        _postService = postService;
    }

    protected override async Task<ApiResponse<GetAllPostsByUserResponseDto>> ProcessAsync(GetAllPostsByUserRequestDto request, CancellationToken cancellationToken) 
    {

        var posts = await _postService.GetUserPostsAsync(request.UserId, cancellationToken);

        if( posts == null || posts.Count == AppConstants.Zero)
        {
            return new ApiResponse<GetAllPostsByUserResponseDto>()
            {
                Success = true,
                Messages = new List<string> { "No any Posts found" },
                Data = null,
            };
        }

        return new ApiResponse<GetAllPostsByUserResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "Posts fetched successfully" },
            Data = new GetAllPostsByUserResponseDto()
            {
                Posts = posts
            },
        };
    }
}
