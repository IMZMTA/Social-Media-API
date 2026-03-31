using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Models;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Infrastructure.Repository.CommentRepository;

public class CommentRepository : ICommentRepository
{
    private readonly IApplicationDbContext _dbContext;
    public CommentRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Comment> CreateAsync(Comment comment, CancellationToken cancellationToken)
    {
        await _dbContext.Comments.AddAsync(comment, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return comment;
    }

    public async Task<int> DeleteAsync(int commentId, int userId, CancellationToken cancellationToken)
    {
        var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId && c.UserId == userId, cancellationToken); 
        
        if (comment == null) 
            return AppConstants.Zero; 

        _dbContext.Comments.Remove(comment); 
        await _dbContext.SaveChangesAsync(cancellationToken);
        return commentId;
    }

    public async Task<CommentModel?> GetByIdAsync(int commentId, CancellationToken cancellationToken)
    {
        return await _dbContext.Comments 
                    .Where(c => c.Id == commentId)
                    .Select(c => new CommentModel 
                    { 
                        CommentId = c.Id, 
                        UserId = c.UserId, 
                        PostId = c.PostId,
                        Content = c.Content,  
                        CreatedAt = c.CreatedAt, 
                        UpdatedAt = c.UpdatedAt
                    }) 
                    .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<CommentModel>> GetByPostAsync(int postId, CancellationToken cancellationToken)
    {
        return await _dbContext.Comments 
                    .Where(c => c.PostId == postId)
                    .Select(c => new CommentModel 
                    { 
                        CommentId = c.Id, 
                        UserId = c.UserId, 
                        Content = c.Content, 
                        PostId =  c.PostId,
                        CreatedAt = c.CreatedAt, 
                        UpdatedAt = c.UpdatedAt
                    })
                    .ToListAsync(cancellationToken);
    }

    public async Task<CommentResponse> UpdateAsync(int commentId, int userId, string content, CancellationToken cancellationToken)
    {
        var commentDb = await _dbContext.Comments.FirstOrDefaultAsync(p => p.Id == commentId && p.UserId == userId, cancellationToken);

        if(commentDb == null)
        {
            return new CommentResponse()
            {
                CommentId = AppConstants.Zero,
                IsSuccess = true
            };
        }

        commentDb.UpdatedAt = DateTime.UtcNow;
        commentDb.Content = content;
        _dbContext.Comments.Update(commentDb);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new CommentResponse()
        {
            CommentId = commentDb.Id,
            IsSuccess = true  
        };
    }
}