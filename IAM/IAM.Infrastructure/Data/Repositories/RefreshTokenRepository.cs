using IAM.Core.Exceptions;
using IAM.Domain.Entities;
using IAM.Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace IAM.Infrastructure.Data.Repositories
{
    public class RefreshTokenRepository(ApplicationDbContext context) : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<RefreshToken?> GetByTokenAsync(Guid token, CancellationToken cancellationToken = default)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken: cancellationToken);
        }

        public async Task<IReadOnlyList<RefreshToken>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.RefreshTokens
                .Where(rt => rt.UserId == userId)
                .OrderByDescending(rt => rt.Expires)
                .AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid token, CancellationToken cancellationToken = default)
        {
            return await _context.RefreshTokens
                .AnyAsync(rt => rt.Token == token, cancellationToken: cancellationToken);
        }

        public async Task AddAsync(RefreshToken token, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.RefreshTokens.AddAsync(token, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException(
                    "Error adding refresh token",
                    nameof(RefreshToken),
                    token.Token,
                    ex);
            }
        }

        public async Task<bool> DeleteAsync(Guid token, CancellationToken cancellationToken = default)
        {
            RefreshToken? refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken: cancellationToken);

            if (refreshToken == null)
                return false;

            _context.RefreshTokens.Remove(refreshToken);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task DeleteAllForUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var tokens = await GetByUserIdAsync(userId, cancellationToken);
            foreach (var token in tokens)
                await DeleteAsync(token.Token, cancellationToken);
        }

        public async Task<bool> IsActive(Guid tokenId)
        {
            var token = await GetByTokenAsync(tokenId);
            return token != null && token.Expires < DateTime.UtcNow;
        }

    }
}
