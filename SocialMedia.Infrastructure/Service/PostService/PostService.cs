using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Models;
using SocialMedia.Infrastructure.Repository.PostRepository;
using SocialMedia.Infrastructure.Service.ContentModerationService;

namespace SocialMedia.Infrastructure.Service.PostService;
public class PostService : IPostService
{
    private readonly IContentModerationService _moderationService;
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository, IContentModerationService moderationService)
    {
        _postRepository = postRepository;
        _moderationService = moderationService;
    }

    public async Task<PostResponse> CreatePost(int userId, string content, CancellationToken cancellationToken)
    {
        if (await _moderationService.ContainsBannedWords(content,cancellationToken))
        {
            return new PostResponse
            {
                PostId = AppConstants.Zero,
                IsSuccess = false
            };
        }

        var post = new Post
        { 
            UserId = userId, 
            Content = content, 
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userId, 
            UpdatedAt = DateTime.UtcNow,
            UpdatedBy = userId
        };

        var createdPost = await _postRepository.CreateAsync(post, cancellationToken);
        return new PostResponse
        {
            PostId = createdPost.Id,
            IsSuccess = true
        };
    }

        public async Task<PostResponse> UpdatePostAsync(int postId, int userId, string content, CancellationToken cancellationToken)
    {
        if (await _moderationService.ContainsBannedWords(content, cancellationToken))
        {
            return new PostResponse
            {
                PostId = AppConstants.Zero,
                IsSuccess = false
            };
        }

        return await _postRepository.UpdateAsync(postId, userId, content, cancellationToken);
    }


    public async Task<List<PostModel>> GetAllPostsAsync(CancellationToken cancellationToken)
    {
        return await _postRepository.GetAllAsync(cancellationToken);
    }

    public async Task<PostModel?> GetPostByIdAsync(int postId, CancellationToken cancellationToken)
    {
        return await _postRepository.GetByIdAsync(postId, cancellationToken);
    }

    public async Task<List<PostModel>> GetUserPostsAsync(int userId, CancellationToken cancellationToken)
    {
        return await _postRepository.GetUserPostsAsync(userId, cancellationToken);
    }

    public async Task<int> RemovePostAsync(int postId, int userId, CancellationToken cancellationToken)
    {
        return await _postRepository.DeleteAsync(postId, userId, cancellationToken);
    }
}
