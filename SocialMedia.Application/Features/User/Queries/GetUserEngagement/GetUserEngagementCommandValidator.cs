using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;

namespace SocialMedia.Application.Features.User.Queries.GetUserEngagement;

public class GetUserEngagementCommandValidator : BaseValidator<GetUserEngagementRequestDto>
{
    public GetUserEngagementCommandValidator() 
    { }
}
