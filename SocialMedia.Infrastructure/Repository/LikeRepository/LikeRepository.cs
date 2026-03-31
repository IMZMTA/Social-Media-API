using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Models;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Infrastructure.Repository.LikeRepository;

public class LikeRepository : ILikeRepository
{
    private readonly IApplicationDbContext _dbContext;
    public LikeRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Like> CreateAsync(Like Like, CancellationToken cancellationToken)
    {
        await _dbContext.Likes.AddAsync(Like, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Like;
    }

    public async Task<bool> DeleteAsync(int likeId, int userId, CancellationToken cancellationToken)
    {
        var like = await _dbContext.Likes.FirstOrDefaultAsync(c => c.Id == likeId && c.UserId == userId, cancellationToken); 
        
        if (like == null) 
            return false; 

        _dbContext.Likes.Remove(like); 
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> ExistsAsync(int postId, int userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Likes.AnyAsync(l => l.PostId == postId && l.UserId == userId, cancellationToken);
    }

    public async Task<LikeModel?> GetByIdAsync(int likeId, CancellationToken cancellationToken)
    {
        return await _dbContext.Likes 
                    .Where(c => c.Id == likeId)
                    .Select(c => new LikeModel 
                    { 
                        LikeId = c.Id, 
                        UserId = c.UserId,
                        PostId = c.PostId,
                    }) 
                    .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<LikeModel>> GetByPostAsync(int postId, CancellationToken cancellationToken)
    {
        return await _dbContext.Likes 
                    .Where(c => c.PostId == postId)
                    .Select(c => new LikeModel 
                    { 
                        LikeId = c.Id, 
                        UserId = c.UserId,
                        PostId =  c.PostId,
                    })
                    .ToListAsync(cancellationToken);
    }
}