using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.Post.Queries.GetAllPosts;

public class GetAllPostsRequestDto : IRequest<ApiResponse<GetAllPostsResponseDto>>
{
}
