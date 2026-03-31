using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.User.Queries.GetAllUsers;

public class GetAllUsersRequestDto : IRequest<ApiResponse<GetAllUsersResponseDto>>
{
}
