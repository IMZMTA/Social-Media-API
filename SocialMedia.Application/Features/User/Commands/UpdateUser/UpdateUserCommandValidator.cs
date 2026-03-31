using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.UserRepository;

namespace SocialMedia.Application.Features.User.Commands.UpdateUser;

public class UpdateUserCommandValidator : BaseValidator<UpdateUserRequestDto>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("UserId must be greater than zero");

        RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Username is required") 
                .MinimumLength(3)
                .WithMessage("Username must be at least 3 characters");
        
        RuleFor(x => x.Email) 
                .NotEmpty()
                .WithMessage("Email is required") 
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .WithMessage("Invalid email format"); 

        RuleFor(x => x)
            .MustAsync(async (request, cancellationToken) =>
            {
                return await _userRepository.ExistsByIdAsync(request.UserId, cancellationToken);
            })
            .WithMessage("User does not exist");
    }
}
