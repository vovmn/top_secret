using AIM.Core.Exceptions;
using AIM.Domain.Entities;
using AIM.Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace AIM.Infrastructure.Data.Repositories
{
    public class RefreshTokenRepository(ApplicationDbContext context) : IRefreshTokenRepository
    {
        public async Task<RefreshToken?> GetByTokenAsync(Guid token, CancellationToken cancellationToken = default)
        {
            return await context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken: cancellationToken);
        }

        public async Task<IReadOnlyList<RefreshToken>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await context.RefreshTokens
                .Where(rt => rt.UserId == userId)
                .OrderByDescending(rt => rt.Expires)
                .AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid token, CancellationToken cancellationToken = default)
        {
            return await context.RefreshTokens
                .AnyAsync(rt => rt.Token == token, cancellationToken: cancellationToken);
        }

        public async Task AddAsync(RefreshToken token, CancellationToken cancellationToken = default)
        {
            try
            {
                await context.RefreshTokens.AddAsync(token, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
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

        public async Task UpdateAsync(RefreshToken token, CancellationToken cancellationToken = default)
        {
            try
            {
                context.RefreshTokens.Update(token);
                await context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException(
                    "Error updating refresh token",
                    nameof(RefreshToken),
                    token.Token,
                    ex);
            }
        }

        public async Task<bool> DeleteAsync(Guid token, CancellationToken cancellationToken = default)
        {
            RefreshToken? refreshToken = await context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken: cancellationToken);

            if (refreshToken == null)
                return false;

            context.RefreshTokens.Remove(refreshToken);

            await context.SaveChangesAsync(cancellationToken);
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
