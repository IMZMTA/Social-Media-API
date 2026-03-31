using SocialMedia.Domain.Models;
using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Service.UserService;

namespace SocialMedia.Application.Features.User.Commands.RegisterUser;

public class RegisterUserHandler : BaseHandler<RegisterUserRequestDto, RegisterUserResponseDto>
{
    private readonly IUserService userService;

    public RegisterUserHandler(IUserService _userService)
    {
        userService = _userService;
    }

    protected override async Task<ApiResponse<RegisterUserResponseDto>> ProcessAsync(RegisterUserRequestDto request, CancellationToken cancellationToken) 
    {
        var userEntity = new SignUpModel
        { 
            UserName = request.UserName, 
            Email = request.Email, 
            Password = request.Password
        };

        var user = await userService.RegisterUser(userEntity, cancellationToken);

        return new ApiResponse<RegisterUserResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "User registered successfully" },
            Data = new RegisterUserResponseDto()
            {
                UserId = user.UserId, 
                UserName = user.UserName, 
                Email = user.Email
            },
        };
    }
}
