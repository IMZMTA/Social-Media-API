using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Service.UserService;

namespace SocialMedia.Application.Features.User.Queries.GetUserById;

public class GetUserByIdHandler : BaseHandler<GetUserByIdRequestDto, GetUserByIdResponseDto>
{
    private readonly IUserService userService;

    public GetUserByIdHandler(IUserService _userService)
    {
        userService = _userService;
    }

    protected override async Task<ApiResponse<GetUserByIdResponseDto>> ProcessAsync(GetUserByIdRequestDto request, CancellationToken cancellationToken)
    {
        var user = await userService.GetById(request.UserId, cancellationToken);

        if (user == null)
        {
            return new ApiResponse<GetUserByIdResponseDto>()
            {
                Success = true,
                Messages = new List<string> { "User not found" },
                Data = null,
            };
        }

        return new ApiResponse<GetUserByIdResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "User fetched successfully" },
            Data = new GetUserByIdResponseDto()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email
            },
        };
    }
}
