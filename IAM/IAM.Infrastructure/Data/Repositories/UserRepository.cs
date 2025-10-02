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
        private readonly ApplicationDbContext _context = context;

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _context.Users
                .Include(u => u.LoginInfo)
                .Include(u => u.FIO)
                .Include(u => u.Messengers)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User?> GetByLoginAsync(string login)
        {
            return await _context.Users
                .Include(u => u.LoginInfo)
                .Include(u => u.FIO)
                .Include(u => u.Messengers)
                .FirstOrDefaultAsync(u => (u.LoginInfo.Username != null && u.LoginInfo.Username == login)
                || (u.LoginInfo.PhoneNumber != null && u.LoginInfo.PhoneNumber == login)
                || (u.LoginInfo.Email != null && u.LoginInfo.Email == login));
        }

        public async Task<User?> GetByLoginInfoAsync(LoginInfo loginInfo)
        {
            return await _context.Users
                .Include(u => u.LoginInfo)
                .Include(u => u.FIO)
                .Include(u => u.Messengers)
                .FirstOrDefaultAsync(u => (u.LoginInfo.Username != null && u.LoginInfo.Username == loginInfo.Username)
                || (u.LoginInfo.PhoneNumber != null && u.LoginInfo.PhoneNumber == loginInfo.PhoneNumber)
                || (u.LoginInfo.Email != null && u.LoginInfo.Email == loginInfo.Email));
        }

        public async Task<IReadOnlyList<User>> GetUsersByPrivelegesAsync(Roles role)
        {
            return await _context.Users
                .Where(u => u.Priveleges == role)
                .Include(u => u.LoginInfo)
                .Include(u => u.FIO)
                .Include(u => u.Messengers)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.LoginInfo)
                .Include(u => u.FIO)
                .Include(u => u.Messengers)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid userId)
        {
            return await _context.Users
                .AnyAsync(u => u.Id == userId);
        }

        public async Task<bool> LoginExistsAsync(string login)
        {
            return await _context.Users
                .AnyAsync(u => (u.LoginInfo.Username != null && u.LoginInfo.Username == login)
                || (u.LoginInfo.PhoneNumber != null && u.LoginInfo.PhoneNumber == login)
                || (u.LoginInfo.Email != null && u.LoginInfo.Email == login));
        }

        public async Task<bool> LoginInfoExistsAsync(LoginInfo loginInfo)
        {
            return await _context.Users
                .AnyAsync(u => (u.LoginInfo.Username != null && u.LoginInfo.Username == loginInfo.Username)
                || (u.LoginInfo.PhoneNumber != null && u.LoginInfo.PhoneNumber == loginInfo.PhoneNumber)
                || (u.LoginInfo.Email != null && u.LoginInfo.Email == loginInfo.Email));
        }

        public async Task AddAsync(User user)
        {
            try
            {
                if (await LoginInfoExistsAsync(user.LoginInfo))
                    throw new ArgumentException("Пользователь уже существует");
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
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
                var existingUser = await _context.Users.FindAsync(user.Id) ?? throw new RepositoryException("User not found");

                // Update only the properties that have changed
                _context.Entry(existingUser).CurrentValues.SetValues(user);

                await _context.SaveChangesAsync();
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
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    throw new RepositoryException("Error deleting user", ex);
                }
            }
        }
    }
}
