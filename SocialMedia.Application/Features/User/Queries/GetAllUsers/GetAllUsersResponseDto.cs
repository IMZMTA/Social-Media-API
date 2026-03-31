using SocialMedia.Domain.Models;

namespace SocialMedia.Application.Features.User.Queries.GetAllUsers;

public class GetAllUsersResponseDto
{
    public List<UserList> Users { get; set; } = new List<UserList>();
}
