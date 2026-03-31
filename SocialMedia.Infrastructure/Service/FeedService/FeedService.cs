using SocialMedia.Domain.Models;
using SocialMedia.Infrastructure.Repository.FeedRepository;

namespace SocialMedia.Infrastructure.Service.FeedService;
public class FeedService : IFeedService
{
    private readonly IFeedRepository _feedRepository;
    public FeedService(IFeedRepository feedRepository)
    {
        _feedRepository = feedRepository;
    }
    public async Task<List<FeedItemModel>> GetFeedAsync(CancellationToken cancellationToken)
    {
        return await _feedRepository.GetFeedAsync(cancellationToken);
    }
}

