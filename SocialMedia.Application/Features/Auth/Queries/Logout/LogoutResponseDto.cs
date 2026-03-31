using SocialMedia.Domain.Models;

namespace SocialMedia.Application.Features.Auth.Queries.Logout;

public class LogoutResponseDto
{
    public int UserId  { get; set; }
    public string? AccessToken { get; set; }
    public string? TokenType { get; set; }
    public long ExpiresIn { get; set; }
    public UserModel? User { get; set; }
}
