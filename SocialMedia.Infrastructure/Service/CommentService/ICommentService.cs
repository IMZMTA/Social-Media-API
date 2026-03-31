using SocialMedia.Domain.Models;

namespace SocialMedia.Infrastructure.Service.CommentService;

public interface ICommentService
{
    Task<CommentResponse> CreateCommentAsync(int postId, int userId, string content, CancellationToken cancellationToken);
    Task<CommentResponse> UpdateCommentAsync(int CommentId, int userId, string content, CancellationToken cancellationToken);
    Task<CommentModel?> GetCommentByIdAsync(int CommentId, CancellationToken cancellationToken); 
    Task<List<CommentModel>> GetPostCommentsAsync(int postId, CancellationToken cancellationToken);
    Task<int> RemoveCommentAsync(int CommentId, int userId, CancellationToken cancellationToken);

}
