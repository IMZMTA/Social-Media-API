using System.Text.Json.Serialization;
using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.User.Commands.UpdateUser;

public class UpdateUserRequestDto : IRequest<ApiResponse<UpdateUserResponseDto>>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
