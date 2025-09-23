using System.ComponentModel.DataAnnotations;

namespace AIM.API.Models.Entities
{
    public class LoginInfo(Guid userId, string username, string? email, string? phoneNumber)
    {

        /// <summary>
        /// Id пользователя (внешний ключ)
        /// </summary>
        public Guid UserId { get; set; } = userId;

        /// <summary>
        /// Сам логин, может генериться автоматически системой.
        /// 1 из оставшихся 2х элементов обязателен.
        /// </summary>
        [Required]
        public string Username { get; private set; } = username;

        public string? Email { get; private set; } = email;

        public string? PhoneNumber { get; private set; } = phoneNumber;

        public bool Contains(string? value)
        {
            return value == Username || value == Email || value == PhoneNumber;
        }
    }
}
