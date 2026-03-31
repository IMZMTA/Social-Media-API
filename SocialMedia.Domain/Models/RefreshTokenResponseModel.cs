namespace SocialMedia.Domain.Models;

public class RefreshTokenResponseModel
{
    public int UserId { get; set; }
    public string? AccessToken { get; set; } = string.Empty;
    public string? TokenType { get; set; } = string.Empty;
    public long ExpiresIn { get; set; }

}
