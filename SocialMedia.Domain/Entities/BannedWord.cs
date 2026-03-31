

using SocialMedia.Domain.Interfaces;

namespace SocialMedia.Domain.Entities;

public class BannedWord : BaseEntity, ITrackEntity
{
    public string Word { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
}
