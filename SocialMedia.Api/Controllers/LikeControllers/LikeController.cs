using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Common;
using SocialMedia.Application.Features.Like.Commands.CreateLike;
using SocialMedia.Application.Features.Like.Commands.RemoveLike;
using SocialMedia.Application.Features.Like.Queries.GetAllLikeByPost;
using SocialMedia.Domain.Constants;

namespace SocialMedia.Api.Controllers.LikeControllers
{
    [Authorize]
    [ApiController]
    [Route(ApiRoutes.Likes.Root)]
    public class LikeController : ApiController
    {

        /// <summary>
        /// Gets a likes of post
        /// </summary>
        [HttpGet("{postId}")]
        [ProducesResponseType(typeof(ApiResponse<GetAllLikeByPostResponseDto>), 200)]
        public async Task<IActionResult> GetById(int postId)
        {
            var result = await Mediator.Send(new GetAllLikeByPostRequestDto { PostId = postId });
            return Ok(result);
        }

        /// <summary>
        /// Like the post
        /// </summary>
        [HttpPost("{postId}")]
        [ProducesResponseType(typeof(ApiResponse<CreateLikeResponseDto>), 200)]
        public async Task<IActionResult> Create(int postId)
        {
            var result = await Mediator.Send(new CreateLikeRequestDto { PostId = postId });
            return Ok(result);
        }
        
        /// <summary>
        /// Unlike the post
        /// </summary>
        [HttpDelete("{likeId}")]
        [ProducesResponseType(typeof(ApiResponse<RemoveLikeResponseDto>), 200)]
        public async Task<IActionResult> Delete(int likeId)
        {
            var result = await Mediator.Send(new RemoveLikeRequestDto { LikeId = likeId });
            return Ok(result);
        }
    }
}
