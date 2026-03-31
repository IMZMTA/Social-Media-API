using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Repository.RefreshTokenRepository;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetAsync(string jti, int userId, CancellationToken cancellationToken);
    Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
    Task RevokeAllAsync(int userId, CancellationToken cancellationToken);
    Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
}
