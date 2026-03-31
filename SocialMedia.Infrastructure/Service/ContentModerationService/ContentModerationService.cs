using SocialMedia.Infrastructure.Service.BannedWordsCacheService;

namespace SocialMedia.Infrastructure.Service.ContentModerationService;

public class ContentModerationService : IContentModerationService
{
    private readonly IBannedWordsCacheService _bannedWordcache;

    public ContentModerationService(IBannedWordsCacheService bannedWordcache)
    {
        _bannedWordcache = bannedWordcache;
    }

    public async Task<bool> ContainsBannedWords(string content, CancellationToken cancellationToken)
    {
        var banned = await _bannedWordcache.GetWords(cancellationToken);

        var words = content.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return words.Any(w => banned.Contains(w));
    }
}
