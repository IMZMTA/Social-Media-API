using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.UserRepository;

namespace SocialMedia.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandValidator : BaseValidator<RefreshTokenRequestDto>
{
    public RefreshTokenCommandValidator(IUserRepository _userRepository)
    {}
}
