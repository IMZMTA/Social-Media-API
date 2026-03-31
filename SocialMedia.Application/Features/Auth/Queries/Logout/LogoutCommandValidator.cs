using SocialMedia.Application.CQRS.Abstractions;

namespace SocialMedia.Application.Features.Auth.Queries.Logout;

public class LogoutCommandValidator : BaseValidator<LogoutRequestDto>
{
    public LogoutCommandValidator() 
    { 
 
    }
}
