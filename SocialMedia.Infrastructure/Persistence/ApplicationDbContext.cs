
using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Interfaces;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } = default!;

        public DbSet<Post> Posts { get; set; } = default!;

        public DbSet<Comment> Comments { get; set; } = default!;

        public DbSet<Like> Likes { get; set; } = default!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
        public DbSet<BannedWord> BannedWords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);// Industry Standard: Index the Token for fast lookups
            modelBuilder.Entity<RefreshToken>().HasIndex(x => x.Jti).IsUnique(false);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<ITrackEntity>())
            {
                if (entry.State == EntityState.Added) 
                { 
                    entry.Entity.CreatedAt = DateTime.UtcNow; 
                    entry.Entity.UpdatedAt = DateTime.UtcNow; 
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
