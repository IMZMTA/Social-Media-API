using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.UserRepository;

namespace SocialMedia.Application.Features.User.Commands.RemoveUser;

public class RemoveUserCommandValidator : BaseValidator<RemoveUserRequestDto>
{
    private readonly IUserRepository _userRepository;

    public RemoveUserCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("UserId must be greater than zero");

        RuleFor(x => x.UserId)
            .MustAsync(async (userId, cancellationToken) =>
            {
                return await _userRepository.ExistsByIdAsync(userId, cancellationToken);
            })
            .WithMessage("User does not exist");
    }
}
