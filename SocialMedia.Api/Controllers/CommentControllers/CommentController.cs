using Microsoft.AspNetCore.Mvc;
using SocialMedia.Domain.Constants;
using SocialMedia.Application.Common;
using Microsoft.AspNetCore.Authorization;
using SocialMedia.Application.Features.Comment.Commands.CreateComment;
using SocialMedia.Application.Features.Comment.Commands.RemoveComment;
using SocialMedia.Application.Features.Comment.Commands.UpdateComment;
using SocialMedia.Application.Features.Comment.Queries.GetAllCommentByPost;
using SocialMedia.Application.Features.Comment.Queries.GetCommentById;

namespace SocialMedia.Api.Controllers.CommentControllers
{
    [Authorize]
    [ApiController]
    [Route(ApiRoutes.Comments.Root)]
    public class CommentController : ApiController
    {
        /// <summary>
        /// Create a new comment
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CreateCommentResponseDto>), 200)]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Updates an comment
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<UpdateCommentResponseDto>), 200)]
        public async Task<IActionResult> Update([FromBody] UpdateCommentRequestDto request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Removes a comment by id
        /// </summary>
        [HttpDelete("{commentId}")]
        [ProducesResponseType(typeof(ApiResponse<RemoveCommentResponseDto>), 200)]
        public async Task<IActionResult> Delete(int commentId)
        {
            var request = new RemoveCommentRequestDto { CommentId = commentId };
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Gets all comment of post
        /// </summary>
        [HttpGet("post/{postId}")]
        [ProducesResponseType(typeof(ApiResponse<List<GetAllCommentByPostRequestDto>>), 200)]
        public async Task<IActionResult> GetAll(int postId)
        {
            var result = await Mediator.Send(new GetAllCommentByPostRequestDto(){PostId = postId});
            return Ok(result);
        }

        /// <summary>
        /// Gets comment by id
        /// </summary>
        [HttpGet("{commentId}")]
        [ProducesResponseType(typeof(ApiResponse<GetCommentByIdResponseDto>), 200)]
        public async Task<IActionResult> GetById(int commentId)
        {
            var result = await Mediator.Send(new GetCommentByIdRequestDto { CommentId = commentId });
            return Ok(result);
        }
    }
}
