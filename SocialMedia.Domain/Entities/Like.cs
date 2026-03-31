using SocialMedia.Domain.Interfaces;

namespace SocialMedia.Domain.Entities;

public class Like : BaseEntity
{
    public int UserId { get; set; }
    public int PostId { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual Post Post { get; set; } = null!;
}