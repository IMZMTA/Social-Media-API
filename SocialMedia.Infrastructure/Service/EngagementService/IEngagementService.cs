using SocialMedia.Domain.Models;

namespace SocialMedia.Infrastructure.Service.EngagementService ;

public interface IEngagementService
{
    Task<List<UserEngagementModel>> GetUserEngagement(CancellationToken cancellationToken);
}
