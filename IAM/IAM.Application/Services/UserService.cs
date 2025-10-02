using AutoMapper;
using IAM.Application.DTOs.Requests;
using IAM.Application.DTOs.Responses;
using IAM.Application.Interfaces;
using IAM.Domain.Entities;
using IAM.Domain.Enums;
using IAM.Infrastructure.Data.Interfaces;
using IAM.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading;

namespace IAM.Application.Services
{
    /// <summary>
    /// Логика управления пользователями
    /// </summary>
    public class UserService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IConfiguration _configuration = configuration;
        private readonly IMapper _mapper = mapper;

        public async Task<UserResponseDto?> GetUser(GetUserRequestDto request)
        {
            User? user;
            if (!string.IsNullOrWhiteSpace(request.Id))
            {
                try
                {
                    user = await _userRepository.GetByIdAsync(Guid.Parse(request.Id));
                    if (user != null)
                        return _mapper.Map<UserResponseDto>(user);
                }
                finally { }
            }
            if (!string.IsNullOrWhiteSpace(request.UserName))
            {
                try
                {
                    user = await _userRepository.GetByLoginAsync(request.UserName);
                    if (user != null)
                        return _mapper.Map<UserResponseDto>(user);
                }
                finally { }
            }
            return null;
        }

        public async Task<IReadOnlyList<UserResponseDto>> GetUsers(GetUsersRequestDto request)
        {
            if (!string.IsNullOrWhiteSpace(request.Role))
            {
                Roles role = (Roles)Enum.Parse(typeof(Roles), request.Role);
                if (role == Roles.ADMIN || role == Roles.NONE)
                    throw new ArgumentException("Недопустимая роль");
                return _mapper.Map<IReadOnlyList<UserResponseDto>>(await _userRepository.GetUsersByPrivelegesAsync(role));
            } 
            return [];
        }

        public async Task<IReadOnlyList<UserResponseDto>> GetAllUsers()
        {
            return _mapper.Map<IReadOnlyList<UserResponseDto>>(await _userRepository.GetAllUsersAsync());
        }
    }
}

               