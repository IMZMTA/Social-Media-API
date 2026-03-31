using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.User.Queries.GetUserById;

public class GetUserByIdRequestDto : IRequest<ApiResponse<GetUserByIdResponseDto>>
{
    public int UserId { get; set; }
}
