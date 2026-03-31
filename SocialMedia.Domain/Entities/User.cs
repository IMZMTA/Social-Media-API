using SocialMedia.Domain.Interfaces;

namespace SocialMedia.Domain.Entities;

public class User : BaseEntity, ITrackEntity
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty; 
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } 
    public int? CreatedBy { get; set; } 
    public DateTime UpdatedAt { get; set; } 
    public int? UpdatedBy { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Like> Likes { get; set; } = new List<Like>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
