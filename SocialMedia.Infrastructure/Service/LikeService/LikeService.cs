using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Models;
using SocialMedia.Infrastructure.Repository.LikeRepository;

namespace SocialMedia.Infrastructure.Service.LikeService;
public class LikeService : ILikeService
{
    private readonly ILikeRepository _likeRepository;

    public LikeService(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

    public async Task<LikeResponse> CreateLikeAsync(int postId, int userId, CancellationToken cancellationToken)
    {
        if (await _likeRepository.ExistsAsync(postId, userId, cancellationToken)){

            return new LikeResponse()
            {
                IsLiked = true
            };
        }
            
        var like = new Like
        { 
            UserId = userId,
            PostId = postId
        };

        var createdLike = await _likeRepository.CreateAsync(like, cancellationToken);
        return new LikeResponse
        {
            LikeId = createdLike.Id,
            IsLiked = true
        };
    }

    public async Task<List<LikeModel>> GetLikesByPostAsync(int postId, CancellationToken cancellationToken)
    {
        return await _likeRepository.GetByPostAsync(postId, cancellationToken);
    }

    public async Task<bool> RemoveLikeAsync(int LikeId, int userId, CancellationToken cancellationToken)
    {
        return await _likeRepository.DeleteAsync(LikeId, userId, cancellationToken);
    }
}
