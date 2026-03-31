using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.PostRepository;

namespace SocialMedia.Application.Features.Post.Commands.RemovePost;

public class RemovePostCommandValidator : BaseValidator<RemovePostRequestDto>
{
    private readonly IPostRepository _postRepository;

    public RemovePostCommandValidator(IPostRepository postRepository)
    {
        _postRepository = postRepository;

        RuleFor(x => x.PostId)
            .GreaterThan(0)
            .WithMessage("PostId must be greater than zero");

        RuleFor(x => x.PostId)
            .MustAsync(async (PostId, cancellationToken) =>
            {
                var post = await _postRepository.GetByIdAsync(PostId, cancellationToken);
                return post != null;
            })
            .WithMessage("Post does not exist");
    }
}
