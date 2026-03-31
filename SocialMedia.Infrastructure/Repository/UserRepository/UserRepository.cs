using SocialMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Infrastructure.Interfaces;
using SocialMedia.Domain.Models;

namespace SocialMedia.Infrastructure.Repository.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly IApplicationDbContext _dbContext;

    public UserRepository(IApplicationDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<User> CreateUser(User user, CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<bool> ExistsByIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.AnyAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task<bool> ExistsByUsernameOrEmailAsync(string userName, string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.AnyAsync(u => u.UserName == userName || u.Email == email, cancellationToken);
    }

    public async Task<List<User>> GetAll(CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .Include(u => u.Posts)
            .Include(u => u.Likes)
            .Include(u => u.Comments)
            .ToListAsync(cancellationToken);
    }


    public async Task<User> GetByEmailorUsername(string userName, string email, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(
            u => u.UserName == userName || u.Email == email,
            cancellationToken) ?? throw new InvalidOperationException($"User with username '{userName}' or email '{email}' not found.");

        return user;
    }

    public async Task<User?> GetByIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
        .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task<User?> RemoveByIdAsync(int userId, CancellationToken cancellationToken)
    {
        var user = await GetByIdAsync(userId, cancellationToken); 
        
        if (user == null) return null;
        _dbContext.Users.Remove(user); 
        await _dbContext.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<User?> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<List<UserList>> GetAllUsers(CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Select(u => new UserList
            {
                UserId = u.Id,
                UserName = u.UserName,
                Email = u.Email
            })
            .ToListAsync(cancellationToken);
    }

}