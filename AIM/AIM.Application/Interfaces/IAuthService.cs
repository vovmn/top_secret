using AIM.Application.DTOs.Requests;
using AIM.Application.DTOs.Responses;
using Microsoft.AspNetCore.Identity.Data;

namespace AIM.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Authenticate(LoginRequestDto request);
        Task<LoginResponseDto> RefreshToken(RefreshTokenRequest request);
        Task<LoginResponseDto> RegisterUser(RegisterRequestDto request);
    }
}