using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Models;
using SocialMedia.Infrastructure.Repository.UserRepository;

namespace SocialMedia.Infrastructure.Service.EngagementService;

public class EngagementService : IEngagementService
{
    private readonly IUserRepository _userRepository;

    public EngagementService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserEngagementModel>> GetUserEngagement(CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAll(cancellationToken);

        var result = users.Select(u =>
        {
            var postsCount = u.Posts.Count;
            var likesCount = u.Likes.Count;
            var commentsCount = u.Comments.Count;

            var score = (postsCount * AppConstants.Five) + (likesCount * AppConstants.Two) + (commentsCount * AppConstants.Three);

            return new UserEngagementModel
            {
                UserId = u.Id,
                UserName = u.UserName,
                EngagementScore = score
            };
        })
        .OrderByDescending(x => x.EngagementScore)
        .ToList();

        return result;
    }
}
