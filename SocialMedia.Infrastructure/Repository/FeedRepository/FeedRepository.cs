using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Models;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Infrastructure.Repository.FeedRepository;

public class FeedRepository : IFeedRepository
{
    private readonly IApplicationDbContext _dbContext;

    public FeedRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<FeedItemModel>> GetFeedAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Posts
            .AsNoTracking()
            .Include(p => p.User)
            .Include(p => p.Likes).ThenInclude(l => l.User)
            .Include(p => p.Comments).ThenInclude(c => c.User)
            .Select(p => new FeedItemModel
            {
                PostId = p.Id,
                PostContent = p.Content,
                UserId = p.UserId,
                UserName = p.User.UserName,

                Likes = p.Likes.Select(l => new FeedLikeModel
                {
                    UserId = l.UserId,
                    UserName = l.User.UserName
                }).ToList(),

                Comments = p.Comments.Select(c => new FeedCommentModel
                {
                    UserId = c.UserId,
                    UserName = c.User.UserName,
                    Content = c.Content
                }).ToList()
            })
            .ToListAsync(cancellationToken);
    }
}
