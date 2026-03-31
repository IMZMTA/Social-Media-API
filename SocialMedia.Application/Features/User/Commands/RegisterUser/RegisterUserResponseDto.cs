namespace SocialMedia.Application.Features.User.Commands.RegisterUser;

public class RegisterUserResponseDto
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
