namespace SocialMedia.Application.Features.User.Queries.GetUserById;

public class GetUserByIdResponseDto
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
