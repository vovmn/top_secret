using AIM.API.Data;
using AIM.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIM.API.Repositories
{
    public class RefreshTokenRepository(ApplicationDbContext context)
    {
        public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await context.RefreshTokens
                .FirstOrDefaultAsync(co => co.Token == token, cancellationToken);
        }

        public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            await context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        }

        public async Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            context.RefreshTokens.Update(refreshToken);
            await Task.CompletedTask;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            RefreshToken? refreshToken = await context.RefreshTokens
                .FirstOrDefaultAsync(co => co.Id == id, cancellationToken);
            if (refreshToken == null)
                return false;
            context.RefreshTokens.Remove(refreshToken);

            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.RefreshTokens.AnyAsync(co => co.Id == id, cancellationToken);
        }
    }
}
