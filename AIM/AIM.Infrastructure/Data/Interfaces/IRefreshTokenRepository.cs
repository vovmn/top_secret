using AIM.Domain.Entities;

namespace AIM.Infrastructure.Data.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token, CancellationToken cancellationToken = default);
        Task DeleteAllForUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid token, CancellationToken cancellationToken = default);
        Task<RefreshToken?> GetByTokenAsync(Guid token, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<RefreshToken>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<bool> IsActive(Guid tokenId);
        Task UpdateAsync(RefreshToken token, CancellationToken cancellationToken = default);
    }
}