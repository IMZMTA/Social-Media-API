namespace SocialMedia.Domain.Models;

public class UserEngagementModel
{
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public long EngagementScore { get; set; } 

}
