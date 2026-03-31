namespace SocialMedia.Infrastructure.Service.ContentModerationService;
public interface IContentModerationService
{
    Task<bool> ContainsBannedWords(string content, CancellationToken cancellationToken);
}
