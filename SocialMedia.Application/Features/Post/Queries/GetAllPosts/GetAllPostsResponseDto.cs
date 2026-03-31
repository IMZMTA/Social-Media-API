using SocialMedia.Domain.Models;

namespace SocialMedia.Application.Features.Post.Queries.GetAllPosts;

public class GetAllPostsResponseDto
{
    public List<PostModel> Posts { get; set; } = new List<PostModel>();
}
