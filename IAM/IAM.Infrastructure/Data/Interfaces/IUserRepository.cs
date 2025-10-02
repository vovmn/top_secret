using IAM.Domain.Entities;
using IAM.Domain.Enums;
using IAM.Domain.ValueObjects;

namespace IAM.Infrastructure.Data.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task DeleteAsync(Guid userId);
        Task<bool> ExistsAsync(Guid userId);
        Task<User?> GetByIdAsync(Guid userId);
        Task<User?> GetByLoginAsync(string login);
        Task<User?> GetByLoginInfoAsync(LoginInfo loginInfo);
        Task<IReadOnlyList<User>> GetUsersByPrivelegesAsync(Roles role);
        Task<IReadOnlyList<User>> GetAllUsersAsync();
        Task<bool> LoginExistsAsync(string login);
        Task<bool> LoginInfoExistsAsync(LoginInfo loginInfo);
        Task PatchAsync(User user);
        Task UpdateFIOAsync(Guid userId, FIO newFIO);
        Task UpdateLoginAsync(Guid userId, LoginInfo newLogin);
        Task UpdateMessengersAsync(Guid userId, Messengers newMessengers);
        Task UpdatePasswordAsync(Guid userId, string newPassword);
        Task UpdatePrivelegesAsync(Guid userId, Roles newPriveleges);
    }
}