using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Models;

namespace SocialMedia.Infrastructure.Repository.LikeRepository;

public interface ILikeRepository
{
    Task<List<LikeModel>> GetByPostAsync(int postId, CancellationToken cancellationToken); 
    Task<LikeModel?> GetByIdAsync(int likeId, CancellationToken cancellationToken); 
    Task<Like> CreateAsync(Like like, CancellationToken cancellationToken); 
    Task<bool> DeleteAsync(int likeId, int userId, CancellationToken cancellationToken); 
    Task<bool> ExistsAsync(int postId, int userId, CancellationToken cancellationToken);
}