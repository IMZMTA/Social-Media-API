using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Domain.Constants;
using SocialMedia.Infrastructure.Service.UserService;

namespace SocialMedia.Application.Features.User.Commands.RemoveUser;

public class RemoveUserHandler : BaseHandler<RemoveUserRequestDto, RemoveUserResponseDto>
{
    private readonly IUserService _userService;

    public RemoveUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    protected override async Task<ApiResponse<RemoveUserResponseDto>> ProcessAsync(
        RemoveUserRequestDto request, 
        CancellationToken cancellationToken)
    {
        var userId = await _userService.RemoveByIdAsync(request.UserId, cancellationToken);

        if (userId ==  AppConstants.Zero)
        {
            return new ApiResponse<RemoveUserResponseDto>()
            {
                Success = false,
                Messages = new List<string> { "User not found or could not be deleted" }
            };
        }

        return new ApiResponse<RemoveUserResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "User deleted successfully" },
            Data = new RemoveUserResponseDto
            {
                UserId = userId
            }
        };
    }
}
