using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.UserRepository;

namespace SocialMedia.Application.Features.Post.Commands.CreatePost;

public class CreatePostCommandValidator : BaseValidator<CreatePostRequestDto>
{
    public CreatePostCommandValidator(IUserRepository _userRepository)
    {}
}
