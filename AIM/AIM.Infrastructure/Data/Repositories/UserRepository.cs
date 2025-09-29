using AIM.Core.Exceptions;
using AIM.Domain.Entities;
using AIM.Domain.Enums;
using AIM.Domain.ValueObjects;
using AIM.Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AIM.Infrastructure.Data.Repositories
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
                .FirstOrDefaultAsync(u => u.LoginInfo.Contains(login));
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

        public async Task<bool> ExistsAsync(Guid userId)
        {
            return await context.Users
                .AnyAsync(u => u.Id == userId);
        }

        public async Task<bool> LoginExistsAsync(LoginInfo loginInfo)
        {
            return await context.Users
                .AnyAsync(u => u.LoginInfo.Contains(loginInfo.Username)
                    || u.LoginInfo.Contains(loginInfo.Email)
                    || u.LoginInfo.Contains(loginInfo.PhoneNumber));
        }

        public async Task AddAsync(User user)
        {
            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Error adding user", ex);
            }
        }

        public async Task UpdateAsync(User user)
        {
            try
            {
                context.Users.Update(user);
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
                await UpdateAsync(user);
            }
        }

        public async Task UpdatePasswordAsync(Guid userId, string newPassword)
        {
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                user.UpdatePassword(newPassword);
                await UpdateAsync(user);
            }
        }

        public async Task UpdateFIOAsync(Guid userId, FIO newFIO)
        {
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                user.UpdateFIO(newFIO);
                await UpdateAsync(user);
            }
        }

        public async Task UpdateMessengersAsync(Guid userId, Messengers newMessengers)
        {
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                user.UpdateMessengers(newMessengers);
                await UpdateAsync(user);
            }
        }

        public async Task UpdatePrivelegesAsync(Guid userId, Roles newPriveleges)
        {
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                user.UpdatePriveleges(newPriveleges);
                await UpdateAsync(user);
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
