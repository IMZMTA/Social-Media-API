using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Models;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Infrastructure.Repository.PostRepository;

public class PostRepository : IPostRepository
{
    private readonly IApplicationDbContext _dbContext;
    public PostRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Post> CreateAsync(Post post, CancellationToken cancellationToken)
    {
        await _dbContext.Posts.AddAsync(post,cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return post;
    }

    public async Task<int> DeleteAsync(int postId, int userId, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId && p.UserId == userId, cancellationToken); 
        
        if (post == null) 
            return AppConstants.Zero; 

        _dbContext.Posts.Remove(post); 
        await _dbContext.SaveChangesAsync(cancellationToken);
        return postId;
    }

    public async Task<List<PostModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Posts 
                    .Include(p => p.Likes) 
                    .Include(p => p.Comments) 
                    .Select(p => new PostModel 
                    { 
                        PostId = p.Id, 
                        UserId = p.UserId, 
                        Content = p.Content, 
                        LikesCount = p.Likes.Count, 
                        CommentsCount = p.Comments.Count, 
                        CreatedAt = p.CreatedAt,
                        UpdatedAt = p.UpdatedAt  
                    })
                    .ToListAsync(cancellationToken);
    }

    public async Task<PostModel?> GetByIdAsync(int postId, CancellationToken cancellationToken)
    {
        return await _dbContext.Posts 
                    .Include(p => p.Likes) 
                    .Include(p => p.Comments)
                    .Where(p=> p.Id == postId)
                    .Select(p => new PostModel 
                    { 
                        PostId = p.Id, 
                        UserId = p.UserId, 
                        Content = p.Content, 
                        LikesCount = p.Likes.Count, 
                        CommentsCount = p.Comments.Count, 
                        CreatedAt = p.CreatedAt,
                        UpdatedAt = p.UpdatedAt 
                    }) 
                    .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<PostModel>> GetUserPostsAsync(int userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Posts 
                    .Where(p => p.UserId == userId)
                    .Include(p => p.Likes) 
                    .Include(p => p.Comments) 
                    .Select(p => new PostModel 
                    { 
                        PostId = p.Id, 
                        UserId = p.UserId, 
                        Content = p.Content, 
                        LikesCount = p.Likes.Count, 
                        CommentsCount = p.Comments.Count, 
                        CreatedAt = p.CreatedAt,
                        UpdatedAt = p.UpdatedAt 
                    })
                    .ToListAsync(cancellationToken);
    }

    public async Task<PostResponse> UpdateAsync(int postId, int userId, string content, CancellationToken cancellationToken)
    {
        var postDb = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId && p.UserId == userId, cancellationToken);

        if(postDb == null)
        {
            return new PostResponse()
            {
                PostId = AppConstants.Zero,
                IsSuccess = true
            };
        }

        postDb.UpdatedAt = DateTime.UtcNow;
        postDb.Content = content;
        _dbContext.Posts.Update(postDb);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new PostResponse()
        {
            PostId = postDb.Id,
            IsSuccess = true  
        };
    }
}