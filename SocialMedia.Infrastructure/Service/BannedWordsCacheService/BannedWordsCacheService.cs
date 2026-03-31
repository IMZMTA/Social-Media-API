
using Microsoft.Extensions.Caching.Memory;
using SocialMedia.Domain.Constants;
using SocialMedia.Infrastructure.Repository.BannedWordsRepository;

namespace SocialMedia.Infrastructure.Service.BannedWordsCacheService;

public class BannedWordsCacheService : IBannedWordsCacheService
{
    private readonly IMemoryCache _cache;
    private readonly IBannedWordsRepository _bannedWordsRepository;

    private const string CacheKey = AppConstants.BannedWordsCache;

    public BannedWordsCacheService(IMemoryCache cache, IBannedWordsRepository bannedWordsRepository)
    {
        _cache = cache;
        _bannedWordsRepository = bannedWordsRepository;
    }

    public async Task<HashSet<string>> GetWords(CancellationToken cancellationToken)
    {
        if (_cache.TryGetValue(CacheKey, out HashSet<string>? words))
        {
            return words!;
        }

        var list = await _bannedWordsRepository.GetAllAsync(cancellationToken);

        words = list.Select(x => x.Word.ToLower()).ToHashSet();
        _cache.Set(CacheKey, words, TimeSpan.FromHours(12));

        return words;
    }

    public Task InvalidateAsync()
    {
        _cache.Remove(CacheKey);
        return Task.CompletedTask;
    }
}
