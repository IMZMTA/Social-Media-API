using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.PostRepository;

namespace SocialMedia.Application.Features.Post.Queries.GetAllPosts;

public class GetAllPostsCommandValidator : BaseValidator<GetAllPostsRequestDto>
{
    public GetAllPostsCommandValidator() 
    { }
}
