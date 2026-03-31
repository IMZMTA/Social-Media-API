namespace SocialMedia.Domain.Models;

public class UserModel
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty; 
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } 
    public int CreatedBy { get; set; } 
    public DateTime UpdatedAt { get; set; } 
    public int UpdatedBy { get; set; }

}