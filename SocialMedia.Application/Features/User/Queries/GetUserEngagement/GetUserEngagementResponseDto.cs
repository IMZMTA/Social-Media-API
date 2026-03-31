using SocialMedia.Domain.Models;

namespace SocialMedia.Application.Features.User.Queries.GetUserEngagement;

public class GetUserEngagementResponseDto
{
    public List<UserEngagementModel> UserManagement { get; set; } = new();
}
