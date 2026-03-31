using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.UserRepository;

namespace SocialMedia.Application.Features.Auth.Queries.LoginUser;

public class LoginUserCommandValidator : BaseValidator<LoginUserRequestDto>
{
    private readonly IUserRepository userRepository;
    public LoginUserCommandValidator(IUserRepository _userRepository) 
    { 
        userRepository = _userRepository;

        RuleFor(x => x.Identifier)
                .NotEmpty()
                .NotNull()
                .WithMessage("Username or Email is required");
        
        RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("Password is required");
        
        RuleFor(x => x)
            .MustAsync(async (request, cancellationToken) => 
            {
                return await userRepository.ExistsByUsernameOrEmailAsync(request.Identifier, request.Identifier, cancellationToken);
            })
            .WithMessage("Username or Email not found ")
            .WithName("Identifier");
 
        }
}
