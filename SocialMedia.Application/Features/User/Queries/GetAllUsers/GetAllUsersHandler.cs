using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Infrastructure.Service.UserService;
using SocialMedia.Domain.Constants;

namespace SocialMedia.Application.Features.User.Queries.GetAllUsers;

public class GetAllUsersHandler : BaseHandler<GetAllUsersRequestDto, GetAllUsersResponseDto>
{
    private readonly IUserService userService;

    public GetAllUsersHandler(IUserService _userService)
    {
        userService = _userService;
    }

    protected override async Task<ApiResponse<GetAllUsersResponseDto>> ProcessAsync(GetAllUsersRequestDto request, CancellationToken cancellationToken) 
    {

        var users = await userService.GetAll(cancellationToken);

        if( users == null || users.Count == AppConstants.Zero)
        {
            return new ApiResponse<GetAllUsersResponseDto>()
            {
                Success = true,
                Messages = new List<string> { "No any users found" },
                Data = null,
            };
        }

        return new ApiResponse<GetAllUsersResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "Users fetched successfully" },
            Data = new GetAllUsersResponseDto()
            {
                Users = users
            },
        };
    }
}
