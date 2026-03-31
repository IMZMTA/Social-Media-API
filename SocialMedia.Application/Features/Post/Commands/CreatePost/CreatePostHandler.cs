using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using Microsoft.AspNetCore.Http;
using SocialMedia.Infrastructure.Service.PostService;
using SocialMedia.Infrastructure.Service.TokenService;
using SocialMedia.Domain.Constants;

namespace SocialMedia.Application.Features.Post.Commands.CreatePost;

public class CreatePostHandler : BaseHandler<CreatePostRequestDto, CreatePostResponseDto>
{
    private readonly IPostService _postService;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreatePostHandler(IPostService postService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _postService = postService;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<ApiResponse<CreatePostResponseDto>> ProcessAsync(CreatePostRequestDto request, CancellationToken cancellationToken) 
    {
        var token = _tokenService.GetTokenModel(_httpContextAccessor);
        int userId = token.UserId ?? AppConstants.Zero;

        var postModel = await _postService.CreatePost(userId, request.Content ?? string.Empty, cancellationToken);

        if( postModel.IsSuccess )
        {
            return new ApiResponse<CreatePostResponseDto>()
            {
                Success = true,
                Messages = new List<string> { "Post created successfully" },
                Data = null,
            };
        }
        return new ApiResponse<CreatePostResponseDto>()
        {
            Success = false,
            Messages = new List<string> { "Post contains banned words and cannot be posted." },
            Data = null
        };
    }
}
