using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Common;
using SocialMedia.Application.Features.User.Commands.RegisterUser;
using SocialMedia.Application.Features.User.Commands.RemoveUser;
using SocialMedia.Application.Features.User.Commands.UpdateUser;
using SocialMedia.Application.Features.User.Queries.GetAllUsers;
using SocialMedia.Application.Features.User.Queries.GetUserById;
using SocialMedia.Application.Features.User.Queries.GetUserEngagement;
using SocialMedia.Domain.Constants;

namespace SocialMedia.Api.Controllers.UserControllers
{
    [ApiController]
    [Route(ApiRoutes.Users.Root)]
    public class UserController : ApiController
    {
        /// <summary>
        /// Registers a new user
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<RegisterUserResponseDto>), 200)]
        public async Task<IActionResult> Create([FromBody] RegisterUserRequestDto request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing user
        /// </summary>
        [Authorize]
        [HttpPut("{userId}")]
        [ProducesResponseType(typeof(ApiResponse<UpdateUserResponseDto>), 200)]
        public async Task<IActionResult> Update(int userId, [FromBody] UpdateUserRequestDto request)
        {
            request.UserId = userId;
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Removes a user by id
        /// </summary>
        [Authorize]
        [HttpDelete("{userId}")]
        [ProducesResponseType(typeof(ApiResponse<RemoveUserResponseDto>), 200)]
        public async Task<IActionResult> Delete(int userId)
        {
            var request = new RemoveUserRequestDto { UserId = userId };
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<GetAllUsersResponseDto>>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllUsersRequestDto());
            return Ok(result);
        }

        /// <summary>
        /// Gets a user by id
        /// </summary>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(ApiResponse<GetUserByIdResponseDto>), 200)]
        public async Task<IActionResult> GetById(int userId)
        {
            var result = await Mediator.Send(new GetUserByIdRequestDto { UserId = userId });
            return Ok(result);
        }

        /// <summary>
        /// Gets a user engagement score
        /// </summary>
        [Authorize]
        [HttpGet("engagement")]
        [ProducesResponseType(typeof(ApiResponse<GetUserEngagementRequestDto>), 200)]
        public async Task<IActionResult> GetEngagement()
        {
            var result = await Mediator.Send(new GetUserEngagementRequestDto());
            return Ok(result);
        }
    }
}
