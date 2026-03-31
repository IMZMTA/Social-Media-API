using SocialMedia.Domain.Interfaces;

namespace SocialMedia.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public string? Jti { get; set; }
    public bool IsRevoked { get; set; }
    public string? ReplacedByToken { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public int UserId { get; set; }
    public virtual User? User { get; set; }
}