using AIM.API.Data;
using AIM.API.Models;
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
    public class JwtTokenService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        public async Task<LoginResponseDto?> Authenticate(LoginRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                return null;
            // TODO: create db context
            Account userAccount = await dbContext.UserAccounts.FirstOrDefaultAsync(x => x.UserName == request.UserName) ?? return null;
            // TODO: create hashing
            if (!PasswordHasherService.VerifyPassword(request.Password, userAccount.Password))
                return null;

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Name, request.UserName)
                ]),
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("JwtConfig:TokenValidityMins")),
                Issuer = configuration["JwtConfig:Issuer"],
                Audience = configuration["JwtConfig:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtConfig:Key"])),
                SecurityAlgorithms.RsaSsaPssSha256),
            };

            JwtSecurityTokenHandler tokenHandler = new();

            return new LoginResponseDto
            {
                UserName = request.UserName,
                AccessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)),
                ExpiresIn = (int)tokenDescriptor.Expires.Value.Subtract(DateTime.UtcNow).TotalSeconds,
            };
        }
    }
}
