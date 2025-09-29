using AIM.Domain.Entities;
using AIM.Domain.Enums;
using AIM.Domain.ValueObjects;

namespace AIM.Infrastructure.Data.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task DeleteAsync(Guid userId);
        Task<bool> ExistsAsync(Guid userId);
        Task<User?> GetByIdAsync(Guid userId);
        Task<User?> GetByLoginAsync(string login);
        Task<IReadOnlyList<User>> GetUsersByPrivelegesAsync(Roles role);
        Task<bool> LoginExistsAsync(LoginInfo loginInfo);
        Task UpdateAsync(User user);
        Task UpdateFIOAsync(Guid userId, FIO newFIO);
        Task UpdateLoginAsync(Guid userId, LoginInfo newLogin);
        Task UpdateMessengersAsync(Guid userId, Messengers newMessengers);
        Task UpdatePasswordAsync(Guid userId, string newPassword);
        Task UpdatePrivelegesAsync(Guid userId, Roles newPriveleges);
    }
}