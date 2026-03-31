using SocialMedia.Domain.Models;
using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Service.UserService;

namespace SocialMedia.Application.Features.Auth.Queries.LoginUser;

public class LoginUserHandler : BaseHandler<LoginUserRequestDto, LoginUserResponseDto>
{
    private readonly IUserService userService;

    public LoginUserHandler(IUserService _userService)
    {
        userService = _userService;
    }

    protected override async Task<ApiResponse<LoginUserResponseDto>> ProcessAsync(LoginUserRequestDto request, CancellationToken cancellationToken) 
    {
        var userEntity = new LogInModel
        { 
            Identifier = request.Identifier,
            Password = request.Password
        };

        var userLoginResponse = await userService.Login(userEntity, cancellationToken);

        return new ApiResponse<LoginUserResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "User Logined successfully" },
            Data = new LoginUserResponseDto()
            {
                UserId = userLoginResponse.UserId,
                AccessToken = userLoginResponse.AccessToken,
                TokenType = userLoginResponse.TokenType,
                ExpiresIn = userLoginResponse.ExpiresIn,
                User = userLoginResponse.User
            },
        };
    }
}
