using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.PostRepository;

namespace SocialMedia.Application.Features.Post.Queries.GetPostById;

public class GetPostByIdCommandValidator : BaseValidator<GetPostByIdRequestDto>
{
    public GetPostByIdCommandValidator() 
    { 

        RuleFor(x => x.PostId)
                .GreaterThan(0)
                .WithMessage("Post Id must be greater than 0");

        }
}
