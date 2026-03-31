using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.Post.Queries.GetAllPostsByUser;

public class GetAllPostsByUserRequestDto : IRequest<ApiResponse<GetAllPostsByUserResponseDto>>
{
    public int UserId { get; set;}
}
