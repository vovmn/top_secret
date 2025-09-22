using AIM.API.Data;
using AIM.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIM.API.Repositories
{
    public class RefreshTokenRepository(ApplicationDbContext context)
    {
        public async Task<RefreshToken?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Users
                .FirstOrDefaultAsync(co => co.Id == id, cancellationToken);
        }

        public async Task<RefreshToken?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await context.Users
                .FirstOrDefaultAsync(co => co.UserName == userName || co.EMail == userName || co.PhoneNumber == userName, cancellationToken);
        }

        public async Task AddAsync(RefreshToken user, CancellationToken cancellationToken = default)
        {
            await context.Users.AddAsync(user, cancellationToken);
        }

        public async Task UpdateAsync(RefreshToken user, CancellationToken cancellationToken = default)
        {
            context.Users.Update(user);
            await Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Users.AnyAsync(co => co.Id == id, cancellationToken);
        }
    }
}
