using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Repository.BannedWordsRepository;

public interface IBannedWordsRepository
{
    Task<List<BannedWord>> GetAllAsync(CancellationToken cancellationToken);
}