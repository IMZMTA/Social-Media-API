using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.Post.Commands.RemovePost;

public class RemovePostRequestDto : IRequest<ApiResponse<RemovePostResponseDto>>
{
    public int PostId { get; set; }
}
