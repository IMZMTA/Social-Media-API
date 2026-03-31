using SocialMedia.Domain.Models;

namespace SocialMedia.Application.Features.Like.Queries.GetAllLikeByPost;

public class GetAllLikeByPostResponseDto
{
    public List<LikeModel> Likes { get; set; } = new List<LikeModel>();
}
