using SocialMedia.Domain.Constants;
using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Service.LikeService;

namespace SocialMedia.Application.Features.Like.Queries.GetAllLikeByPost;

public class GetAllLikeByPostHandler : BaseHandler<GetAllLikeByPostRequestDto, GetAllLikeByPostResponseDto>
{
    private readonly ILikeService _likeService;

    public GetAllLikeByPostHandler(ILikeService likeService)
    {
        _likeService = likeService;
    }

    protected override async Task<ApiResponse<GetAllLikeByPostResponseDto>> ProcessAsync(GetAllLikeByPostRequestDto request, CancellationToken cancellationToken) 
    {

        var Likes = await _likeService.GetLikesByPostAsync(request.PostId, cancellationToken);

        if( Likes == null || Likes.Count == AppConstants.Zero)
        {
            return new ApiResponse<GetAllLikeByPostResponseDto>()
            {
                Success = true,
                Messages = new List<string> { "No any Like" },
                Data = null,
            };
        }

        return new ApiResponse<GetAllLikeByPostResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "Likes fetched successfully" },
            Data = new GetAllLikeByPostResponseDto()
            {
                Likes = Likes
            },
        };
    }
}
