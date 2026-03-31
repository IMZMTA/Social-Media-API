using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;

namespace SocialMedia.Application.Features.Comment.Queries.GetCommentById;

public class GetCommentByIdCommandValidator : BaseValidator<GetCommentByIdRequestDto>
{
    public GetCommentByIdCommandValidator() 
    { 

        RuleFor(x => x.CommentId)
                .GreaterThan(0)
                .WithMessage("Comment Id must be greater than 0");

        }
}
