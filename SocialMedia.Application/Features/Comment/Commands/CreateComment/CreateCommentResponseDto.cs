namespace SocialMedia.Application.Features.Comment.Commands.CreateComment;

public class CreateCommentResponseDto
{
    public int UserId { get; set; }
    public string? AccessToken { get; set; } = string.Empty;
    public string? TokenType { get; set; }
    public long ExpiresIn { get; set; }
}
