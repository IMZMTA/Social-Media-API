using SocialMedia.Domain.Interfaces;

namespace SocialMedia.Domain.Entities;
public class Comment : BaseEntity, ITrackEntity
{
    public string Content { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int PostId { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual Post Post { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
}