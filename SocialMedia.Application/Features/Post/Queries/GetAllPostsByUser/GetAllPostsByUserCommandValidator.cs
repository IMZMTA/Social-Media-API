using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.PostRepository;
using SocialMedia.Infrastructure.Repository.UserRepository;

namespace SocialMedia.Application.Features.Post.Queries.GetAllPostsByUser;

public class GetAllPostsByUserCommandValidator : BaseValidator<GetAllPostsByUserRequestDto>
{
    private readonly IUserRepository _userRepository;
    public GetAllPostsByUserCommandValidator(IUserRepository userRepository)
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
