using Microsoft.AspNetCore.Http;
using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Domain.Constants;
using SocialMedia.Infrastructure.Service.PostService;
using SocialMedia.Infrastructure.Service.TokenService;

namespace SocialMedia.Application.Features.Post.Commands.RemovePost;

public class RemovePostHandler : BaseHandler<RemovePostRequestDto, RemovePostResponseDto>
{
    private readonly IPostService _postService;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemovePostHandler(IPostService postService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _postService = postService;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<ApiResponse<RemovePostResponseDto>> ProcessAsync(
        RemovePostRequestDto request, 
        CancellationToken cancellationToken)
    {
        var token = _tokenService.GetTokenModel(_httpContextAccessor);
        int userId = token.UserId ?? AppConstants.Zero;
        var postId = await _postService.RemovePostAsync(request.PostId, userId, cancellationToken);

        if (postId ==  AppConstants.Zero)
        {
            return new ApiResponse<RemovePostResponseDto>()
            {
                Success = false,
                Messages = new List<string> { "Post not found or you are not authorized to delete this post" }
            };
        }

        return new ApiResponse<RemovePostResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "Post deleted successfully" },
            Data = new RemovePostResponseDto
            {
                PostId = postId
            }
        };
    }
}
