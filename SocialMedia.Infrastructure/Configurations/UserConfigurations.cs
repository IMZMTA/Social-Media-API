// SocialMedia.Infrastructure/Configurations/UserConfigurations.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Configurations;

// SocialMedia.Infrastructure/Configurations/UserConfiguration.cs
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();

        builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Password).IsRequired();

        builder.Property(u => u.CreatedAt).IsRequired();
        builder.Property(u => u.CreatedBy).IsRequired(false);
        builder.Property(u => u.UpdatedAt).IsRequired();
        builder.Property(u => u.UpdatedBy).IsRequired(false);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.HasMany(u => u.Posts).WithOne(p => p.User).HasForeignKey(p => p.UserId);
        builder.HasMany(u => u.Comments).WithOne(c => c.User).HasForeignKey(c => c.UserId);
        builder.HasMany(u => u.Likes).WithOne(l => l.User).HasForeignKey(l => l.UserId);
        builder.HasMany(u => u.RefreshTokens).WithOne(rt => rt.User).HasForeignKey(rt => rt.UserId);
    }
}