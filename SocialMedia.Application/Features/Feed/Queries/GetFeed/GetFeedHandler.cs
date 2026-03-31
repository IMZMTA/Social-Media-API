using SocialMedia.Domain.Constants;
using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Service.FeedService;

namespace SocialMedia.Application.Features.Feed.Queries.GetFeed;

public class GetFeedHandler : BaseHandler<GetFeedRequestDto, GetFeedResponseDto>
{
    private readonly IFeedService _feedService;

    public GetFeedHandler(IFeedService feedService)
    {
        _feedService = feedService;
    }

    protected override async Task<ApiResponse<GetFeedResponseDto>> ProcessAsync(GetFeedRequestDto request, CancellationToken cancellationToken) 
    {

        var feeds = await _feedService.GetFeedAsync(cancellationToken);

        if( feeds == null || feeds.Count == AppConstants.Zero)
        {
            return new ApiResponse<GetFeedResponseDto>()
            {
                Success = true,
                Messages = new List<string> { "No any feeds" },
                Data = null,
            };
        }

        return new ApiResponse<GetFeedResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "Feeds fetched successfully" },
            Data = new GetFeedResponseDto()
            {
                Feeds = feeds
            },
        };
    }
}
