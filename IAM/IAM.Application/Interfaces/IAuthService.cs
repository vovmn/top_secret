using IAM.Application.DTOs.Requests;
using IAM.Application.DTOs.Responses;
using Microsoft.AspNetCore.Identity.Data;

namespace IAM.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Authenticate(LoginRequestDto request);
        Task<LoginResponseDto> RefreshToken(RefreshTokenRequest request);
        Task<LoginResponseDto> RegisterUser(RegisterRequestDto request);
    }
}