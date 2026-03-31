
namespace SocialMedia.Infrastructure.Service.BannedWordsCacheService;

public interface IBannedWordsCacheService
{
    Task<HashSet<string>> GetWords(CancellationToken cancellationToken);
    Task InvalidateAsync();
}
