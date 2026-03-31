namespace SocialMedia.Domain.Models;

public class FeedItemModel
{
    public int PostId { get; set; }
    public string PostContent { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public List<FeedLikeModel> Likes { get; set; } = new();
    public List<FeedCommentModel> Comments { get; set; } = new();
}


public class FeedCommentModel
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}

public class FeedLikeModel
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
}
