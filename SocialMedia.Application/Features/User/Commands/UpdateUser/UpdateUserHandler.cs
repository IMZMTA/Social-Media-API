using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Models;
using SocialMedia.Infrastructure.Service.UserService;

namespace SocialMedia.Application.Features.User.Commands.UpdateUser;

public class UpdateUserHandler : BaseHandler<UpdateUserRequestDto, UpdateUserResponseDto>
{
    private readonly IUserService _userService;

    public UpdateUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    protected override async Task<ApiResponse<UpdateUserResponseDto>> ProcessAsync(
        UpdateUserRequestDto request, 
        CancellationToken cancellationToken)
    {
        var userModel = new UserModel
        {
            UserId = request.UserId,
            Email = request.Email,
            UserName = request.UserName,
        };
        var user = await _userService.UpdateByIdAsync(userModel, cancellationToken);

        if (user ==  null)
        {
            return new ApiResponse<UpdateUserResponseDto>()
            {
                Success = false,
                Messages = new List<string> { "User not found or could not be deleted" }
            };
        }

        return new ApiResponse<UpdateUserResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "User updated successfully" },
            Data = new UpdateUserResponseDto
            {
                UserId = user.UserId
            }
        };
    }
}
