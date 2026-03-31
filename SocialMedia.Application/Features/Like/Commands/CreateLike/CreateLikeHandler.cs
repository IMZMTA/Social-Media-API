using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using Microsoft.AspNetCore.Http;
using SocialMedia.Infrastructure.Service.LikeService;
using SocialMedia.Infrastructure.Service.TokenService;
using SocialMedia.Domain.Constants;

namespace SocialMedia.Application.Features.Like.Commands.CreateLike;

public class CreateLikeHandler : BaseHandler<CreateLikeRequestDto, CreateLikeResponseDto>
{
    private readonly ILikeService _likeService;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateLikeHandler(ILikeService likeService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _likeService = likeService;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<ApiResponse<CreateLikeResponseDto>> ProcessAsync(CreateLikeRequestDto request, CancellationToken cancellationToken) 
    {
        var token = _tokenService.GetTokenModel(_httpContextAccessor);
        int userId = token.UserId ?? AppConstants.Zero;

        var likeModel = await _likeService.CreateLikeAsync(request.PostId, userId, cancellationToken);

        if( !likeModel.IsLiked )
        {
            return new ApiResponse<CreateLikeResponseDto>()
            {
                Success = true,
                Messages = new List<string> { "Like created successfully" },
                Data = new CreateLikeResponseDto()
                {
                    IsAlreadyLiked = likeModel.IsLiked
                },
            };
        }
        return new ApiResponse<CreateLikeResponseDto>()
        {
            Success = false,
            Messages = new List<string> { "You already liked this post" },
            Data = new CreateLikeResponseDto()
            {
                IsAlreadyLiked = likeModel.IsLiked
            }
        };
    }
}
