using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;

namespace SocialMedia.Application.Features.Feed.Queries.GetFeed;

public class GetFeedCommandValidator : BaseValidator<GetFeedRequestDto>
{
    public GetFeedCommandValidator()
    {

    }
}
