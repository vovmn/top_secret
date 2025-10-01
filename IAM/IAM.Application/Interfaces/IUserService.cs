using IAM.Application.DTOs.Requests;
using IAM.Application.DTOs.Responses;

namespace IAM.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto?> GetUser(GetUserRequestDto request);
        Task<IReadOnlyList<UserResponseDto>> GetUsers(GetUsersRequestDto request);
        Task<IReadOnlyList<UserResponseDto>> GetAllUsers();
    }
}