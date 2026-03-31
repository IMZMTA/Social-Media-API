using SocialMedia.Application.Common;
using SocialMedia.Application.CQRS.Abstractions;
using Microsoft.AspNetCore.Http;
using SocialMedia.Infrastructure.Service.CommentService;
using SocialMedia.Infrastructure.Service.TokenService;
using SocialMedia.Domain.Constants;

namespace SocialMedia.Application.Features.Comment.Commands.CreateComment;

public class CreateCommentHandler : BaseHandler<CreateCommentRequestDto, CreateCommentResponseDto>
{
    private readonly ICommentService _CommentService;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateCommentHandler(ICommentService CommentService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _CommentService = CommentService;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<ApiResponse<CreateCommentResponseDto>> ProcessAsync(CreateCommentRequestDto request, CancellationToken cancellationToken) 
    {
        var token = _tokenService.GetTokenModel(_httpContextAccessor);
        int userId = token.UserId ?? AppConstants.Zero;

        var commentModel = await _CommentService.CreateCommentAsync(request.PostId, userId, request.Content ?? string.Empty, cancellationToken);

        if( commentModel.IsSuccess )
        {
            return new ApiResponse<CreateCommentResponseDto>()
            {
                Success = true,
                Messages = new List<string> { "Comment created successfully" },
                Data = null,
            };
        }
        return new ApiResponse<CreateCommentResponseDto>()
        {
            Success = false,
            Messages = new List<string> { "Comment contains banned words and cannot be commented." },
            Data = null
        };
    }
}
