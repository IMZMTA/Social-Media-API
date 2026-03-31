using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Models;

namespace SocialMedia.Infrastructure.Repository.PostRepository;

public interface IPostRepository
{
    Task<Post> CreateAsync(Post post,CancellationToken cancellationToken);
    Task<PostResponse> UpdateAsync(int postId, int userId, string content, CancellationToken cancellationToken);
    Task<List<PostModel>> GetAllAsync(CancellationToken cancellationToken); 
    Task<PostModel?> GetByIdAsync(int postId, CancellationToken cancellationToken); 
    Task<List<PostModel>> GetUserPostsAsync(int userId, CancellationToken cancellationToken);
    Task<int> DeleteAsync(int postId, int userId, CancellationToken cancellationToken);
}