using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Post> Posts { get; }
        DbSet<Comment> Comments { get; }
        DbSet<Like> Likes { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<BannedWord> BannedWords { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
