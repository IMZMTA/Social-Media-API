using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.User.Queries.GetUserEngagement;

public class GetUserEngagementRequestDto : IRequest<ApiResponse<GetUserEngagementResponseDto>>
{
}
