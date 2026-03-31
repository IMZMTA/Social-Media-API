using Microsoft.AspNetCore.Http;
using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using SocialMedia.Domain.Constants;
using SocialMedia.Infrastructure.Service.CommentService;
using SocialMedia.Infrastructure.Service.TokenService;

namespace SocialMedia.Application.Features.Comment.Commands.UpdateComment;

public class UpdateCommentHandler : BaseHandler<UpdateCommentRequestDto, UpdateCommentResponseDto>
{
    private readonly ICommentService _commentService;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateCommentHandler(ICommentService commentService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _commentService = commentService;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<ApiResponse<UpdateCommentResponseDto>> ProcessAsync(
        UpdateCommentRequestDto request, 
        CancellationToken cancellationToken)
    {
        
        var token = _tokenService.GetTokenModel(_httpContextAccessor);
        int userId = token.UserId ?? AppConstants.Zero;
        var updatedComment = await _commentService.UpdateCommentAsync(request.CommentId, userId, request.Content, cancellationToken);

        if ( updatedComment.CommentId == AppConstants.Zero)
        {
            return new ApiResponse<UpdateCommentResponseDto>()
            {
                Success = false,
                Messages = new List<string> { updatedComment.IsSuccess ? "Comment not found or you are not authorized to update this Comment" : "Comment contains banned words and cannot be commented." }
            };
        }

        return new ApiResponse<UpdateCommentResponseDto>()
        {
            Success = true,
            Messages = new List<string> { "Comment updated successfully" },
            Data = new UpdateCommentResponseDto
            {
                CommentId = updatedComment.CommentId
            }
        };
    }
}
