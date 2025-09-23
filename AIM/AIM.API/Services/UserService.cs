using AIM.API.Models.Entities;
using AIM.API.Repositories;
using System.Threading;

namespace AIM.API.Services
{
    /// <summary>
    /// Логика управления пользователями
    /// </summary>
    public class UserService(UserRepository userRepository, IConfiguration configuration)
    {
        public class 
        if (GetByAnyLoginAsync(user.LoginInfo.Username, cancellationToken) is not null)
    return BadRequest("WTF");
if (GetByAnyLoginAsync(user.LoginInfo.Email, cancellationToken) is not null)
    return BadRequest("WTF");
if (GetByAnyLoginAsync(user.LoginInfo.PhoneNumber, cancellationToken) is not null)
    }
}

               