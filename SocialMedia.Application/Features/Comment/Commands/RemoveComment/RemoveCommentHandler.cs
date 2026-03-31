using Microsoft.AspNetCore.Http;
using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Domain.Constants;
using SocialMedia.Infrastructure.Service.CommentService;
using SocialMedia.Infrastructure.Service.TokenService;

namespace SocialMedia.Application.Features.Comment.Commands.RemoveComment;

public class RemoveCommentHandler : BaseHandler<RemoveCommentRequestDto, RemoveCommentResponseDto>
{
    private readonly ICommentService _commentService;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemoveCommentHandler(ICommentService commentService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _commentService = commentService;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<ApiResponse<RemoveCommentResponseDto>> ProcessAsync(
        RemoveCommentRequestDto request, 
        CancellationToken cancellationToken)
    {
        var token = _tokenService.GetTokenModel(_httpContextAccessor);
        int userId = token.UserId ?? AppConstants.Zero;
        var commentId = await _commentService.RemoveCommentAsync(request.CommentId, userId, cancellationToken);

        if (commentId ==  AppConstants.Zero)
        {
            return new ApiResponse<RemoveCommentResponseDto>()
            {
                Success = false,
                Messages = new List<string> { "Comment not found or you are not authorized to delete this Comment" }
            };
        }

        return new ApiResponse<RemoveCommentResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "Comment deleted successfully" },
            Data = new RemoveCommentResponseDto
            {
                CommentId = commentId
            }
        };
    }
}
