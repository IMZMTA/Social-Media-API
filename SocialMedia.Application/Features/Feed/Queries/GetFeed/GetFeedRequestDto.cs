using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.Feed.Queries.GetFeed;

public class GetFeedRequestDto : IRequest<ApiResponse<GetFeedResponseDto>>
{
    public int PostId { get; set; }
}
