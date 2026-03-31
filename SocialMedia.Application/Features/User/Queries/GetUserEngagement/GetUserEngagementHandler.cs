using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Domain.Constants;
using SocialMedia.Infrastructure.Service.EngagementService;

namespace SocialMedia.Application.Features.User.Queries.GetUserEngagement;

public class GetUserEngagementHandler : BaseHandler<GetUserEngagementRequestDto, GetUserEngagementResponseDto>
{
    private readonly IEngagementService _engagementService;

    public GetUserEngagementHandler(IEngagementService engagementService)
    {
        _engagementService = engagementService;
    }

    protected override async Task<ApiResponse<GetUserEngagementResponseDto>> ProcessAsync(GetUserEngagementRequestDto request, CancellationToken cancellationToken)
    {
        var userManagement = await _engagementService.GetUserEngagement(cancellationToken);

        if ( userManagement == null || userManagement.Count == AppConstants.Zero)
        {
            return new ApiResponse<GetUserEngagementResponseDto>()
            {
                Success = true,
                Messages = new List<string> { "No users found" },
                Data = new GetUserEngagementResponseDto()
            };
        }

        return new ApiResponse<GetUserEngagementResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "User engagement scores retrieved successfully" },
            Data = new GetUserEngagementResponseDto
            {
                UserManagement = userManagement
            }
        };
    }

}
