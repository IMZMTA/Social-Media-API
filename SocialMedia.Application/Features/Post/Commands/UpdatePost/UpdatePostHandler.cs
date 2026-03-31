using Microsoft.AspNetCore.Http;
using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Domain.Constants;
using SocialMedia.Infrastructure.Service.PostService;
using SocialMedia.Infrastructure.Service.TokenService;

namespace SocialMedia.Application.Features.Post.Commands.UpdatePost;

public class UpdatePostHandler : BaseHandler<UpdatePostRequestDto, UpdatePostResponseDto>
{
    private readonly IPostService _postService;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdatePostHandler(IPostService postService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _postService = postService;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<ApiResponse<UpdatePostResponseDto>> ProcessAsync(
        UpdatePostRequestDto request, 
        CancellationToken cancellationToken)
    {
        var token = _tokenService.GetTokenModel(_httpContextAccessor);
        int userId = token.UserId ?? AppConstants.Zero;
        var updatedPost = await _postService.UpdatePostAsync(request.PostId, userId, request.Content, cancellationToken);

        if ( updatedPost.PostId == AppConstants.Zero)
        {
            return new ApiResponse<UpdatePostResponseDto>()
            {
                Success = false,
                Messages = new List<string> { updatedPost.IsSuccess ? "Post not found or you are not authorized to update this post" : "Post contains banned words and cannot be posted." }
            };
        }

        return new ApiResponse<UpdatePostResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "Post updated successfully" },
            Data = new UpdatePostResponseDto
            {
                PostId = updatedPost.PostId
            }
        };
    }
}
