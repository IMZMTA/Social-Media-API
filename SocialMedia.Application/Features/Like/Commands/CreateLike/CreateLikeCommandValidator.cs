using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.UserRepository;

namespace SocialMedia.Application.Features.Like.Commands.CreateLike;

public class CreateLikeCommandValidator : BaseValidator<CreateLikeRequestDto>
{
    public CreateLikeCommandValidator(IUserRepository _userRepository)
    {}
}
