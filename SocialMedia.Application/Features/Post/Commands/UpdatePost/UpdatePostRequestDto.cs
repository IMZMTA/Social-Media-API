using System.Text.Json.Serialization;
using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.Post.Commands.UpdatePost;

public class UpdatePostRequestDto : IRequest<ApiResponse<UpdatePostResponseDto>>
{
    [JsonIgnore]
    public int PostId { get; set; }
    public string Content { get; set; } = string.Empty;
}
