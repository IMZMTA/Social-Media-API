using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.UserRepository;

namespace SocialMedia.Application.Features.Comment.Queries.GetAllCommentByPost;

public class GetAllCommentByPostCommandValidator : BaseValidator<GetAllCommentByPostRequestDto>
{
    private readonly IUserRepository _userRepository;
    public GetAllCommentByPostCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.PostId)
            .GreaterThan(0)
            .WithMessage("PostId must be greater than zero");

    }
}
