using SocialMedia.Domain.Models;

namespace SocialMedia.Application.Features.Comment.Queries.GetAllCommentByPost;

public class GetAllCommentByPostResponseDto
{
    public List<CommentModel> Comments { get; set; } = new List<CommentModel>();
}
