using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Models;

namespace SocialMedia.Infrastructure.Repository.UserRepository;

public interface IUserRepository
{
    Task<User> CreateUser(User user, CancellationToken cancellationToken);
    Task<bool> ExistsByUsernameOrEmailAsync(string userName, string email, CancellationToken cancellationToken);
    Task<User> GetByEmailorUsername(string userName, string email, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(int userId , CancellationToken cancellationToken);
    Task<List<User>> GetAll(CancellationToken cancellationToken);
    Task<bool> ExistsByIdAsync(int userId, CancellationToken cancellationToken);
    Task<User?> RemoveByIdAsync(int userId, CancellationToken cancellationToken);
    Task<User?> UpdateAsync(User user, CancellationToken cancellationToken);
    Task<List<UserList>> GetAllUsers(CancellationToken cancellationToken);
}