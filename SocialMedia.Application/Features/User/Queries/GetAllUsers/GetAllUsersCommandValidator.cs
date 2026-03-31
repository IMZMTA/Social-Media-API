using FluentValidation;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Repository.UserRepository;

namespace SocialMedia.Application.Features.User.Queries.GetAllUsers;

public class GetAllUsersCommandValidator : BaseValidator<GetAllUsersRequestDto>
{
    public GetAllUsersCommandValidator() 
    { }
}
