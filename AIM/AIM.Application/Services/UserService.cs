using AIM.Domain.Entities;
using AIM.Infrastructure.Data.Repositories;
using AIM.Application.DTOs.Requests;
using AIM.Application.DTOs.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Threading;
using Microsoft.Extensions.Configuration;
using AIM.Application.Interfaces;
using AIM.Infrastructure.Data.Interfaces;

namespace AIM.Application.Services
{
    /// <summary>
    /// Логика управления пользователями
    /// </summary>
    public class UserService(IUserRepository userRepository, IConfiguration configuration) : IUserService
    {
        public void test() { }

    }
}

               