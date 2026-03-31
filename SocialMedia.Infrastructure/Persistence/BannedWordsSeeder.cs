using SocialMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Infrastructure.Persistence;

public static class BannedWordsSeeder
{
    private static readonly string[] DefaultBannedWords = new string[]
    {
        "monolith","spaghettiCode","goto","hack","architrixs","quickAndDirty",
        "cowboy","yo","globalVariable","recursiveHell","backdoor","hotfix",
        "leakyAbstraction","mockup","singleton","silverBullet","technicalDebt"
    };

    public static async Task SeedAsync(IApplicationDbContext context, CancellationToken cancellationToken = default)
    {
        if (!await context.BannedWords.AnyAsync(cancellationToken))
        {
            foreach (var word in DefaultBannedWords)
            {
                context.BannedWords.Add(new BannedWord
                {
                    Word = word,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
            }
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
