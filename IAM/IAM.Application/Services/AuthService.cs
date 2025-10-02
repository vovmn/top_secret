using AutoMapper;
using IAM.Application.DTOs.Requests;
using IAM.Application.DTOs.Responses;
using IAM.Application.Interfaces;
using IAM.Domain.Entities;
using IAM.Domain.Enums;
using IAM.Domain.ValueObjects;
using IAM.Infrastructure.Data.Interfaces;
using IAM.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RTools_NTS.Util;
using System;
using System.Data;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace IAM.Application.Services
{
    /// <summary>
    /// Аутентификация, авторизация и генерация JWT
    /// </summary>
    public class AuthService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IConfiguration configuration, IMapper mapper) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
        private readonly IConfiguration _configuration = configuration;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<LoginResponseDto> RegisterUser(RegisterRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Role))
                throw new ArgumentException("Пользователю не задана роль");

            Roles role = (Roles)Enum.Parse(typeof(Roles), request.Role);
            if (role == Roles.ADMIN || role == Roles.NONE)
                throw new ArgumentException("Данную роль невозможно зарегестрировать");

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("Не задан пароль!");

            User newUser = new User(
                        Guid.NewGuid(),
                        new(request.UserName, request.Contacts.Email, request.Contacts.Phone),
                        PasswordHasherService.HashPassword(request.Password),
                        new(request.Name, request.Sername, request.Fathername),
                        new(request.Contacts.WhatsApp, request.Contacts.VK, request.Contacts.Max, request.Contacts.Telegram, request.Contacts.Other),
                        (Roles)Enum.Parse(typeof(Roles), request.Role!)
                    );

            await _userRepository.AddAsync(newUser);

            return await GenerateJwtToken(newUser);
        }

        /// <summary>
        /// Аунтефикация
        /// </summary>
        /// <param name="request">Любой логин (username, email, phone) + пароль</param>
        /// <returns>jwt токен с ролью</returns>
        /// <exception cref="Exception">По разным причинам, проще прочитать (+ могут с репозитория или энтити прилететь)</exception>
        public async Task<LoginResponseDto> Authenticate(LoginRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("Введите данные.");

            User userAccount = await _userRepository.GetByLoginAsync(request.UserName)
                ?? throw new ArgumentException("Неверный логин или пароль.");

            if (!PasswordHasherService.VerifyPassword(request.Password, userAccount.Password))
                throw new ArgumentException("Неверный логин или пароль.");

            if (userAccount.Priveleges == Roles.BANNED)
                throw new AccessViolationException("Вы были заблокированны.");

            return await GenerateJwtToken(userAccount);
        }

        /// <summary>
        /// Обновление токена
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<LoginResponseDto> RefreshToken(RefreshTokenRequest request)
        {
            if (request.RefreshToken == null)
                throw new ArgumentException("отсутствует токен для обновления.");
            // Проверить Guid token? (Парсинг хехе)
            RefreshToken refreshToken = await _refreshTokenRepository.GetByTokenAsync(Guid.Parse(request.RefreshToken))
                ?? throw new AccessViolationException("Токен не действителен");

            await _refreshTokenRepository.DeleteAsync(refreshToken.Token);

            if (refreshToken.Validate())
                throw new AccessViolationException("Токен не действителен");

            User user = await _userRepository.GetByIdAsync(refreshToken.UserId)
               ?? throw new AccessViolationException("Пользователь был удалён");
            return await GenerateJwtToken(user);
        }

        /// <summary>
        /// Отвечает за генерацию jwt токена и прикладывание его в виде ответа
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<LoginResponseDto> GenerateJwtToken(User user)
        {
            JwtSecurityToken token = new(
                _configuration["JwtConfig:Issuer"],
                _configuration["JwtConfig:Audience"],
                [
                    new Claim(JwtRegisteredClaimNames.Name, user.LoginInfo.Username!),
                    new Claim(ClaimTypes.Role, user.Priveleges.ToString())
                ],
                expires: DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JwtConfig:TokenValidityMins")),

                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtConfig:Key"]!)),
                SecurityAlgorithms.HmacSha512Signature));

            JwtSecurityTokenHandler tokenHandler = new();

            return new LoginResponseDto
            {
                UserName = user.LoginInfo.Username,
                AccessToken = tokenHandler.WriteToken(token),
                ExpiresIn = (int)token.ValidTo.Subtract(DateTime.UtcNow).TotalSeconds,
                RefreshToken = await GenerateRefreshToken(user.Id),
            };
        }

        /// <summary>
        /// Отвечает за генерацию токена для обьновления jwt
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<string> GenerateRefreshToken(Guid userId)
        {
            var reftokvalmins = _configuration.GetValue<int>("JwtConfig:RefreshTokenValidity");

            var refreshToken = new RefreshToken(Guid.NewGuid(), DateTime.UtcNow.AddMinutes(reftokvalmins), userId);

            await _refreshTokenRepository.AddAsync(refreshToken);
            return refreshToken.Token.ToString();
        }
    }
}
