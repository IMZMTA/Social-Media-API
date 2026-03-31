namespace SocialMedia.Domain.Models;

public class LogInResponseModel
{
    public int UserId  { get; set; }
    public string? AccessToken { get; set; }
    public string? TokenType { get; set; }
    public long ExpiresIn { get; set; }
    public UserModel? User { get; set; }
}
