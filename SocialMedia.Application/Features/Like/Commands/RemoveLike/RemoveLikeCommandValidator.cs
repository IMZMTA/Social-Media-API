using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.LikeRepository;

namespace SocialMedia.Application.Features.Like.Commands.RemoveLike;

public class RemoveLikeCommandValidator : BaseValidator<RemoveLikeRequestDto>
{
    private readonly ILikeRepository _LikeRepository;

    public RemoveLikeCommandValidator(ILikeRepository LikeRepository)
    {
        _LikeRepository = LikeRepository;

        RuleFor(x => x.LikeId)
            .GreaterThan(0)
            .WithMessage("LikeId must be greater than zero");

        RuleFor(x => x.LikeId)
            .MustAsync(async (LikeId, cancellationToken) =>
            {
                var Like = await _LikeRepository.GetByIdAsync(LikeId, cancellationToken);
                return Like != null;
            })
            .WithMessage("No Liked");
    }
}
