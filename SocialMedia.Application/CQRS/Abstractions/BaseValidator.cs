using FluentValidation;

namespace SocialMedia.Application.CQRS.Abstractions;

public abstract class BaseValidator<T> : AbstractValidator<T>
{
    protected BaseValidator()
    {
    }
}
