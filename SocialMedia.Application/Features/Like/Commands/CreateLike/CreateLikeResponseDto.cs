namespace SocialMedia.Application.Features.Like.Commands.CreateLike;

public class CreateLikeResponseDto
{
    public int LikeId { get; set; }
    public bool IsAlreadyLiked { get; set; }
}
