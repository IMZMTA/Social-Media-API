using Microsoft.AspNetCore.Mvc;
using SocialMedia.Domain.Constants;
using SocialMedia.Application.Common;
using SocialMedia.Application.Features.Post.Commands.CreatePost;
using SocialMedia.Application.Features.Post.Commands.RemovePost;
using SocialMedia.Application.Features.Post.Commands.UpdatePost;
using SocialMedia.Application.Features.Post.Queries.GetAllPosts;
using SocialMedia.Application.Features.Post.Queries.GetPostById;
using SocialMedia.Application.Features.User.Commands.UpdateUser;
using SocialMedia.Application.Features.Post.Queries.GetAllPostsByUser;
using Microsoft.AspNetCore.Authorization;

namespace SocialMedia.Api.Controllers.PostControllers
{
    [Authorize]
    [ApiController]
    [Route(ApiRoutes.Posts.Root)]
    public class PostController : ApiController
    {
        /// <summary>
        /// Registers a new user
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CreatePostResponseDto>), 200)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequestDto request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing user
        /// </summary>
        [HttpPut("{postId}")]
        [ProducesResponseType(typeof(ApiResponse<UpdateUserResponseDto>), 200)]
        public async Task<IActionResult> Update(int postId, [FromBody] UpdatePostRequestDto request)
        {
            request.PostId = postId;
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Removes a user by id
        /// </summary>
        [HttpDelete("{postId}")]
        [ProducesResponseType(typeof(ApiResponse<RemovePostResponseDto>), 200)]
        public async Task<IActionResult> Delete(int postId)
        {
            var request = new RemovePostRequestDto { PostId = postId };
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Gets all posts
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<GetAllPostsResponseDto>>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllPostsRequestDto());
            return Ok(result);
        }

        /// <summary>
        /// Gets a post by id
        /// </summary>
        [HttpGet("{postId}")]
        [ProducesResponseType(typeof(ApiResponse<GetPostByIdResponseDto>), 200)]
        public async Task<IActionResult> GetById(int postId)
        {
            var result = await Mediator.Send(new GetPostByIdRequestDto { PostId = postId });
            return Ok(result);
        }

        /// <summary>
        /// Gets all post by user
        /// </summary>
        [HttpGet("users/{userId}")]
        [ProducesResponseType(typeof(ApiResponse<GetAllPostsByUserResponseDto>), 200)]
        public async Task<IActionResult> GetAllPostsByUser(int userId)
        {
            var result = await Mediator.Send(new GetAllPostsByUserRequestDto { UserId = userId });
            return Ok(result);
        }
    }
}
