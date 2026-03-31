using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.CommentRepository;

namespace SocialMedia.Application.Features.Comment.Commands.RemoveComment;

public class RemoveCommentCommandValidator : BaseValidator<RemoveCommentRequestDto>
{
    private readonly ICommentRepository _CommentRepository;

    public RemoveCommentCommandValidator(ICommentRepository CommentRepository)
    {
        _CommentRepository = CommentRepository;

        RuleFor(x => x.CommentId)
            .GreaterThan(0)
            .WithMessage("CommentId must be greater than zero");

        RuleFor(x => x.CommentId)
            .MustAsync(async (CommentId, cancellationToken) =>
            {
                var Comment = await _CommentRepository.GetByIdAsync(CommentId, cancellationToken);
                return Comment != null;
            })
            .WithMessage("Comment does not exist");
    }
}
