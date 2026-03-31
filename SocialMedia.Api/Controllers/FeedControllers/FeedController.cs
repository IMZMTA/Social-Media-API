using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Common;
using SocialMedia.Application.Features.Feed.Queries.GetFeed;
using SocialMedia.Domain.Constants;

namespace SocialMedia.Api.Controllers.FeedControllers
{
    [Authorize]
    [ApiController]
    [Route(ApiRoutes.Feed.Root)]
    public class FeedController : ApiController
    {
        /// <summary>
        /// Get feeds
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<GetFeedResponseDto>), 200)]
        public async Task<IActionResult> Feed()
        {
            var result = await Mediator.Send(new GetFeedRequestDto());
            return Ok(result);
        }
    }
}
