using IAM.Core.Exceptions;
using IAM.Domain.Entities;
using IAM.Domain.Enums;
using IAM.Domain.ValueObjects;
using IAM.Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IAM.Infrastructure.Data.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await context.Users
                .Include(u => u.LoginInfo)
                .Include(u => u.FIO)
                .Include(u => u.Messengers)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User?> GetByLoginAsync(string login)
        {
            return await context.Users
                .Include(u => u.LoginInfo)
                .Include(u => u.FIO)
                .Include(u => u.Messengers)
                .FirstOrDefaultAsync(u => u.LoginInfo.Username == login ||
            u.LoginInfo.PhoneNumber == login ||
            u.LoginInfo.Email == login);
        }

        public async Task<IReadOnlyList<User>> GetUsersByPrivelegesAsync(Roles role)
        {
            return await context.Users
                .Where(u => u.Priveleges == role)
                .Include(u => u.LoginInfo)
                .Include(u => u.FIO)
                .Include(u => u.Messengers)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<User>> GetAllUsersAsync()
        {
            return await context.Users
                .Include(u => u.LoginInfo)
                .Include(u => u.FIO)
                .Include(u => u.Messengers)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid userId)
        {
            return await context.Users
                .AnyAsync(u => u.Id == userId);
        }

        public async Task<bool> LoginExistsAsync(LoginInfo loginInfo)
        {
            return await context.Users
                .AnyAsync(u => u.LoginInfo.Username == loginInfo.Username
                    || u.LoginInfo.Email == loginInfo.Email
                    || u.LoginInfo.PhoneNumber == loginInfo.PhoneNumber);
        }

        public async Task AddAsync(User user)
        {
            try
            {
                if (await LoginExistsAsync(user.LoginInfo))
                    throw new ArgumentException("Пользователь уже существует");
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error adding user", ex);
            }
        }

        public async Task PatchAsync(User user)
        {
            try
            {
                var existingUser = await context.Users.FindAsync(user.Id) ?? throw new RepositoryException("User not found");

                // Update only the properties that have changed
                context.Entry(existingUser).CurrentValues.SetValues(user);

                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error updating user", ex);
            }
        }

        public async Task UpdateLoginAsync(Guid userId, LoginInfo newLogin)
        {
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                user.UpdateLoginInfo(newLogin);
                await PatchAsync(user);
            }
        }

        public async Task UpdatePasswordAsync(Guid userId, string newPassword)
        {
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                user.UpdatePassword(newPassword);
                await PatchAsync(user);
            }
        }

        public async Task UpdateFIOAsync(Guid userId, FIO newFIO)
        {
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                user.UpdateFIO(newFIO);
                await PatchAsync(user);
            }
        }

        public async Task UpdateMessengersAsync(Guid userId, Messengers newMessengers)
        {
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                user.UpdateMessengers(newMessengers);
                await PatchAsync(user);
            }
        }

        public async Task UpdatePrivelegesAsync(Guid userId, Roles newPriveleges)
        {
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                user.UpdatePriveleges(newPriveleges);
                await PatchAsync(user);
            }
        }

        public async Task DeleteAsync(Guid userId)
        {
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                try
                {
                    context.Users.Remove(user);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    throw new RepositoryException("Error deleting user", ex);
                }
            }
        }
    }
}
