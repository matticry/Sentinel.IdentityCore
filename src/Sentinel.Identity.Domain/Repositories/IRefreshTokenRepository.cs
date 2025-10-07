using Sentinel.Identity.Domain.Entities;

namespace Sentinel.Identity.Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
    Task<IEnumerable<RefreshToken>> GetActiveByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<RefreshToken> AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
    Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
    Task RevokeAllByUserIdAsync(int userId, string revokedByIp, CancellationToken cancellationToken = default);
}