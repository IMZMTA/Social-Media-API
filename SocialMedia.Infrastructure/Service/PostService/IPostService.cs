using SocialMedia.Domain.Models;

namespace SocialMedia.Infrastructure.Service.PostService;

public interface IPostService
{
    Task<PostResponse> CreatePost(int userId, string content, CancellationToken cancellationToken);
    Task<PostResponse> UpdatePostAsync(int postId, int userId, string content, CancellationToken cancellationToken);
    Task<List<PostModel>> GetAllPostsAsync(CancellationToken cancellationToken); 
    Task<PostModel?> GetPostByIdAsync(int postId, CancellationToken cancellationToken); 
    Task<List<PostModel>> GetUserPostsAsync(int userId, CancellationToken cancellationToken);
    Task<int> RemovePostAsync(int postId, int userId, CancellationToken cancellationToken);

}
