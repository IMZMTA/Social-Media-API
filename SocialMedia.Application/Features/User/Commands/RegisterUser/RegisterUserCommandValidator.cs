using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.UserRepository;

namespace SocialMedia.Application.Features.User.Commands.RegisterUser;

public class RegisterUserCommandValidator : BaseValidator<RegisterUserRequestDto>
{
    private readonly IUserRepository userRepository;
    public RegisterUserCommandValidator(IUserRepository _userRepository) 
    { 
        userRepository = _userRepository;

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
        
        RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*(),.?""{}|<>]).{6,}$")
                .WithMessage("Password must be at least 6 characters and contain uppercase, lowercase, digit, and special character");
        
        RuleFor(x => x)
            .MustAsync(async (request, cancellationToken) => 
            {
                return !await userRepository.ExistsByUsernameOrEmailAsync(request.UserName, request.Email,cancellationToken);
            })
            .WithMessage("Username or Email already exist")
            .WithName("Email");
 
        }
}
