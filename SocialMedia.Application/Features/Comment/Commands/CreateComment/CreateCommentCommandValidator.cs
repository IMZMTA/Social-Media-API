using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.UserRepository;

namespace SocialMedia.Application.Features.Comment.Commands.CreateComment;

public class CreateCommentCommandValidator : BaseValidator<CreateCommentRequestDto>
{
    public CreateCommentCommandValidator(IUserRepository _userRepository)
    {}
}
