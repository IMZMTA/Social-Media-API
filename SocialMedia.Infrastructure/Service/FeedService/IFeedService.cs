using SocialMedia.Domain.Models;

namespace SocialMedia.Infrastructure.Service.FeedService;

public interface IFeedService
{
    Task<List<FeedItemModel>> GetFeedAsync(CancellationToken cancellationToken);
}
