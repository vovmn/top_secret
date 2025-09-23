using AIM.API.Services;
using System.ComponentModel.DataAnnotations;

namespace AIM.API.Models.Entities
{
    public class User(Guid id, LoginInfo loginInfo, string password, FIO fIO, Messangers messangers, Roles priveleges)
    {

        /// <summary>
        /// Уникальный идентификатор объекта.
        /// </summary>
        public Guid Id { get; private set; } = id;

        /// <summary>
        /// Возможные логины для аутентификации пользователя.
        /// </summary>
        public LoginInfo LoginInfo { get; private set; } = loginInfo;

        /// <summary>
        /// Пароль от аккаунта.
        /// </summary>
        [Required]
        public string Password { get; private set; } = password;

        /// <summary>
        /// Фио пользователя
        /// </summary>
        public FIO FIO { get; private set; } = fIO;

        /// <summary>
        /// Мессенджеры для контакта с пользователем (ПОТОМ ПРОДАДИМ! :3)
        /// </summary>
        public Messangers Messangers { get; private set; } = messangers;

        /// <summary>
        /// Роль пользователя в системе.
        /// </summary>
        [Required]
        public Roles Priveleges { get; private set; } = priveleges;

    }
}
