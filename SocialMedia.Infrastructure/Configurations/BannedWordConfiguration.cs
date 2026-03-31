using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Constants;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Configurations;

public class BannedWordConfiguration : IEntityTypeConfiguration<BannedWord>
{
    public void Configure(EntityTypeBuilder<BannedWord> builder)
    {
        builder.ToTable(TableNames.BannedWord);

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Property(b => b.Word).IsRequired().HasMaxLength(100);

        builder.Property(b => b.CreatedAt).IsRequired();
        builder.Property(b => b.CreatedBy).IsRequired(false);
        builder.Property(b => b.UpdatedAt).IsRequired();
        builder.Property(b => b.UpdatedBy).IsRequired(false);

        builder.HasIndex(b => b.Word).IsUnique();
    }
}
