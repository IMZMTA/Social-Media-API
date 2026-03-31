using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.CommentRepository;

namespace SocialMedia.Application.Features.Comment.Commands.UpdateComment;

public class UpdateCommentCommandValidator : BaseValidator<UpdateCommentRequestDto>
{
    private readonly ICommentRepository _CommentRepository;

    public UpdateCommentCommandValidator(ICommentRepository CommentRepository)
    {
        _CommentRepository = CommentRepository;

        RuleFor(x => x.CommentId)
            .GreaterThan(0)
            .WithMessage("CommentId must be greater than zero");

        RuleFor(x => x.Content) 
            .NotEmpty() 
            .WithMessage("Content cannot be empty");

        RuleFor(x => x.CommentId)
            .MustAsync(async (CommentId, cancellationToken) =>
            {
                var Comment = await _CommentRepository.GetByIdAsync(CommentId, cancellationToken);
                return Comment != null;
            })
            .WithMessage("Comment does not exist");
        
    }
}
