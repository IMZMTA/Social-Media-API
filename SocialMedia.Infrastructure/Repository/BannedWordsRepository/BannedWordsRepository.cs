using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Entities;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Infrastructure.Repository.BannedWordsRepository;

public class BannedWordsRepository : IBannedWordsRepository
{
    private readonly IApplicationDbContext _dbContext;
    public BannedWordsRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<BannedWord>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.BannedWords.ToListAsync(cancellationToken);
    }
}