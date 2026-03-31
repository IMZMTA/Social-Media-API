using SocialMedia.Domain.Models;

namespace SocialMedia.Infrastructure.Service.LikeService;

public interface ILikeService
{
    Task<LikeResponse> CreateLikeAsync(int postId, int userId, CancellationToken cancellationToken);
    Task<List<LikeModel>> GetLikesByPostAsync(int postId, CancellationToken cancellationToken);
    Task<bool> RemoveLikeAsync(int LikeId, int userId, CancellationToken cancellationToken);

}
