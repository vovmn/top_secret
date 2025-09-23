using AIM.API.Data;
using AIM.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIM.API.Repositories
{
    public class UserRepository(ApplicationDbContext context)
    {
        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Users
                .FirstOrDefaultAsync(co => co.Id == id, cancellationToken);
        }

        public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await context.Users
                .FirstOrDefaultAsync(co => co.UserName == userName || co.EMail == userName || co.PhoneNumber == userName, cancellationToken);
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await context.Users.AddAsync(user, cancellationToken);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            context.Users.Update(user);
            await Task.CompletedTask;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            User? user = await context.Users
                .FirstOrDefaultAsync(co => co.Id == id, cancellationToken);
            if (user == null)
                return false;
            context.Users.Remove(user);

            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Users.AnyAsync(co => co.Id == id, cancellationToken);
        }

        // ADD VALIDATION
    }
}
