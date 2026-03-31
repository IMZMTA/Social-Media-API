using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Common;
using SocialMedia.Application.Features.Auth.Commands.RefreshToken;
using SocialMedia.Application.Features.Auth.Queries.LoginUser;
using SocialMedia.Application.Features.Auth.Queries.Logout;
using SocialMedia.Domain.Constants;

namespace SocialMedia.Api.Controllers.AuthController
{
    [ApiController]
    [Route(ApiRoutes.Auth.Root)]
    public class AuthController : ApiController
    {
        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <param name="request">Refresh Token details</param>
        /// <returns> ApiResponse for Refresh token response</returns>
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(ApiResponse<RefreshTokenResponseDto>), 200)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="request">User Login details</param>
        /// <returns>Return the login user info</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<LoginUserResponseDto>), 200)]
        public async Task<IActionResult> Login([FromBody] LoginUserRequestDto request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="request">User Logout details</param>
        /// <returns>Return the logout info</returns>
        [HttpPost("logout")]
        [ProducesResponseType(typeof(ApiResponse<LogoutResponseDto>), 200)]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDto request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }
    }
}
