using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Entities;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Infrastructure.Repository.RefreshTokenRepository;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly IApplicationDbContext _dbContext;
    public RefreshTokenRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RefreshToken?> GetAsync(string jti, int userId, CancellationToken cancellationToken)
        => await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Jti == jti && rt.UserId == userId, cancellationToken);

    public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        _dbContext.RefreshTokens.Add(refreshToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RevokeAllAsync(int userId, CancellationToken cancellationToken)
    {
        var tokens = _dbContext.RefreshTokens.Where(rt => rt.UserId == userId && !rt.IsRevoked);
        foreach (var token in tokens)
        {
            token.IsRevoked = true;
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        _dbContext.RefreshTokens.Update(refreshToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
