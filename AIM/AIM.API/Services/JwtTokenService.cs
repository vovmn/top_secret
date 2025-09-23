using AIM.API.Data;
using AIM.API.Models;
using AIM.API.Models.Entities;
using AIM.API.Repositories;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace AIM.API.Services
{
    /// <summary>
    /// Генерация JWT
    /// </summary>
    public class JwtTokenService(UserRepository userRepository, RefreshTokenRepository refreshTokenRepository, IConfiguration configuration)
    {
        public async Task<LoginResponseDto?> Authenticate(LoginRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                return null;
            // TODO: check password for sql enjectios
            User? userAccount = await userRepository.GetByUserNameAsync(request.UserName);
            // TODO: create hashing
            if (userAccount is null || !PasswordHasherService.VerifyPassword(request.Password, userAccount.Password))
                return null;

            return await GenerateJwtToken(userAccount);
        }
        public async Task<LoginResponseDto?> ValidateRefreshToken(string token)
        {
            RefreshToken? refreshToken = await refreshTokenRepository.GetByTokenAsync(token);
            if (refreshToken is null || refreshToken.Expires < DateTime.UtcNow) return null;
            await refreshTokenRepository.DeleteAsync(refreshToken.Id);
            var user = await userRepository.GetByIdAsync(refreshToken.Id);
            if (user is null) return null;

            return await GenerateJwtToken(user);
        }


        public async Task<LoginResponseDto> GenerateJwtToken(User user)
        {
            JwtSecurityToken token = new(
                configuration["JwtConfig:Issuer"],
                configuration["JwtConfig:Audience"],
                [
                    new Claim(JwtRegisteredClaimNames.Name, user.UserName!)
                ],
                expires: DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("JwtConfig:TokenValidityMins")),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtConfig:Key"])),
                SecurityAlgorithms.HmacSha512Signature));

            JwtSecurityTokenHandler tokenHandler = new();

            return new LoginResponseDto
            {
                UserName = user.UserName,
                AccessToken = tokenHandler.WriteToken(token),
                ExpiresIn = (int)token.ValidTo.Subtract(DateTime.UtcNow).TotalSeconds,
                RefreshToken = await GenerateRefreshToken(user.Id),
            };
        }

        private async Task<string> GenerateRefreshToken(Guid userId)
        {
            var reftokvalmins = configuration.GetValue<int>("JwtConfig:RefreshTokenValidity");
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expires = DateTime.UtcNow.AddMinutes(reftokvalmins),
                Id = userId
            };
            await refreshTokenRepository.AddAsync(refreshToken);

            return refreshToken.Token;
        }
    }
}
