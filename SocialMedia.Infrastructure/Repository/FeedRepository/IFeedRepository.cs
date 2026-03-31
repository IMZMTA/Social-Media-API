using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Models;

namespace SocialMedia.Infrastructure.Repository.FeedRepository;

public interface IFeedRepository
{
    Task<List<FeedItemModel>> GetFeedAsync(CancellationToken cancellationToken);
}