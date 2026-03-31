using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.UserRepository;

namespace SocialMedia.Application.Features.Like.Queries.GetAllLikeByPost;

public class GetAllLikeByPostCommandValidator : BaseValidator<GetAllLikeByPostRequestDto>
{
    private readonly IUserRepository _userRepository;
    public GetAllLikeByPostCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.PostId)
            .GreaterThan(0)
            .WithMessage("PostId must be greater than zero");
    }
}
