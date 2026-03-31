using SocialMedia.Domain.Models;

namespace SocialMedia.Application.Features.Feed.Queries.GetFeed;

public class GetFeedResponseDto
{
    public List<FeedItemModel> Feeds { get; set; } = new List<FeedItemModel>();
}
