using SocialMedia.Domain.Models;

namespace SocialMedia.Application.Features.Post.Queries.GetAllPostsByUser;

public class GetAllPostsByUserResponseDto
{
    public List<PostModel> Posts { get; set; } = new List<PostModel>();
}
