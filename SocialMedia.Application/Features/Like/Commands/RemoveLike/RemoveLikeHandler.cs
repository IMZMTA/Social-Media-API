using Microsoft.AspNetCore.Http;
using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Domain.Constants;
using SocialMedia.Infrastructure.Service.LikeService;
using SocialMedia.Infrastructure.Service.TokenService;

namespace SocialMedia.Application.Features.Like.Commands.RemoveLike;

public class RemoveLikeHandler : BaseHandler<RemoveLikeRequestDto, RemoveLikeResponseDto>
{
    private readonly ILikeService _LikeService;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemoveLikeHandler(ILikeService LikeService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _LikeService = LikeService;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<ApiResponse<RemoveLikeResponseDto>> ProcessAsync(
        RemoveLikeRequestDto request, 
        CancellationToken cancellationToken)
    {
        var token = _tokenService.GetTokenModel(_httpContextAccessor);
        int userId = token.UserId ?? AppConstants.Zero;
        var success = await _LikeService.RemoveLikeAsync(request.LikeId, userId, cancellationToken);

        return new ApiResponse<RemoveLikeResponseDto>()
        {
            Success = success,
            Messages = new List<string> { success ? "Like removed successfully" : "Like not found or unauthorized"},
            Data = null
        };
    }
}
