namespace SocialMedia.Domain.Interfaces;

public interface ITrackUpdated
{
    DateTime UpdatedAt { get; set; }
    int? UpdatedBy { get; set; }
}
