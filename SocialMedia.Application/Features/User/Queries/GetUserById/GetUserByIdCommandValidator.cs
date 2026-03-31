using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.UserRepository;

namespace SocialMedia.Application.Features.User.Queries.GetUserById;

public class GetUserByIdCommandValidator : BaseValidator<GetUserByIdRequestDto>
{
    public GetUserByIdCommandValidator() 
    { 

        RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("User Id must be greater than 0");
        }
}
