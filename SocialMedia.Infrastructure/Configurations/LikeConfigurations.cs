using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Configurations;
public class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.ToTable(TableNames.Likes);
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).ValueGeneratedOnAdd();

        builder.Property(l => l.UserId).IsRequired();
        builder.Property(l => l.PostId).IsRequired();

        builder.HasOne(l => l.User).WithMany(u => u.Likes).HasForeignKey(l => l.UserId);
        builder.HasOne(l => l.Post).WithMany(p => p.Likes).HasForeignKey(l => l.PostId);

        builder.HasIndex(l => new { l.UserId, l.PostId }).IsUnique();
    }
}