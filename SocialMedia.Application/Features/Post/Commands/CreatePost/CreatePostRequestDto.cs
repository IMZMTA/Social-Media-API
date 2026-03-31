using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Features.Post.Commands.CreatePost;

public class CreatePostRequestDto : IRequest<ApiResponse<CreatePostResponseDto>>
{
    public string? Content { get; set; }
}
