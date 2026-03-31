using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.PostRepository;

namespace SocialMedia.Application.Features.Post.Commands.UpdatePost;

public class UpdatePostCommandValidator : BaseValidator<UpdatePostRequestDto>
{
    private readonly IPostRepository _postRepository;

    public UpdatePostCommandValidator(IPostRepository postRepository)
    {
        _postRepository = postRepository;

        RuleFor(x => x.PostId)
            .GreaterThan(0)
            .WithMessage("PostId must be greater than zero");

        RuleFor(x => x.Content) 
            .NotEmpty() 
            .WithMessage("Content cannot be empty");

        RuleFor(x => x.PostId)
            .MustAsync(async (PostId, cancellationToken) =>
            {
                var post = await _postRepository.GetByIdAsync(PostId, cancellationToken);
                return post != null;
            })
            .WithMessage("Post does not exist");
        
    }
}
