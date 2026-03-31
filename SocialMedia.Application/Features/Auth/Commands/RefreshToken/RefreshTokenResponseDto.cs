namespace SocialMedia.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenResponseDto
{
    public int UserId { get; set; }
    public string? AccessToken { get; set; } = string.Empty;
    public string? TokenType { get; set; }
    public long ExpiresIn { get; set; }
}
