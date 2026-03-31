using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Models;
using SocialMedia.Infrastructure.Repository.CommentRepository;
using SocialMedia.Infrastructure.Service.ContentModerationService;

namespace SocialMedia.Infrastructure.Service.CommentService;
public class CommentService : ICommentService
{
    private readonly IContentModerationService _moderationService;
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository CommentRepository, IContentModerationService moderationService)
    {
        _commentRepository = CommentRepository;
        _moderationService = moderationService;
    }

    public async Task<CommentResponse> CreateCommentAsync(int postId, int userId, string content, CancellationToken cancellationToken)
    {
        if (await _moderationService.ContainsBannedWords(content,cancellationToken))
        {
            return new CommentResponse
            {
                CommentId = AppConstants.Zero,
                IsSuccess = false
            };
        }

        var comment = new Comment
        { 
            UserId = userId,
            PostId = postId, 
            Content = content, 
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userId, 
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = userId
        };

        var createdComment = await _commentRepository.CreateAsync(comment, cancellationToken);
        return new CommentResponse
        {
            CommentId = createdComment.Id,
            IsSuccess = true
        };
    }

    public async Task<CommentResponse> UpdateCommentAsync(int commentId, int userId, string content, CancellationToken cancellationToken)
    {
        if (await _moderationService.ContainsBannedWords(content, cancellationToken))
        {
            return new CommentResponse
            {
                CommentId = AppConstants.Zero,
                IsSuccess = false
            };
        }

        return await _commentRepository.UpdateAsync(commentId, userId, content, cancellationToken);
    }

    public async Task<CommentModel?> GetCommentByIdAsync(int commentId, CancellationToken cancellationToken)
    {
        return await _commentRepository.GetByIdAsync(commentId, cancellationToken);
    }

    public async Task<List<CommentModel>> GetPostCommentsAsync(int userId, CancellationToken cancellationToken)
    {
        return await _commentRepository.GetByPostAsync(userId, cancellationToken);
    }

    public async Task<int> RemoveCommentAsync(int CommentId, int userId, CancellationToken cancellationToken)
    {
        return await _commentRepository.DeleteAsync(CommentId, userId, cancellationToken);
    }
}
