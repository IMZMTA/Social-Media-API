namespace SocialMedia.Application.Features.Post.Commands.CreatePost;

public class CreatePostResponseDto
{
    public int UserId { get; set; }
    public string? AccessToken { get; set; } = string.Empty;
    public string? TokenType { get; set; }
    public long ExpiresIn { get; set; }
}
