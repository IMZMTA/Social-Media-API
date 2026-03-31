namespace SocialMedia.Application.Features.Comment.Queries.GetCommentById;

public class GetCommentByIdResponseDto
{
    public int CommentId { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
