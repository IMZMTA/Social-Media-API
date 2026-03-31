using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Models;

namespace SocialMedia.Infrastructure.Repository.CommentRepository;

public interface ICommentRepository
{
    Task<Comment> CreateAsync(Comment Comment,CancellationToken cancellationToken);
    Task<CommentResponse> UpdateAsync(int commentId, int userId, string content, CancellationToken cancellationToken);
    Task<CommentModel?> GetByIdAsync(int CommentId, CancellationToken cancellationToken); 
    Task<List<CommentModel>> GetByPostAsync(int postId, CancellationToken cancellationToken);
    Task<int> DeleteAsync(int CommentId, int userId, CancellationToken cancellationToken);
}