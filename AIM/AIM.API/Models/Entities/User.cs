using AIM.API.Services;
using System.ComponentModel.DataAnnotations;

namespace AIM.API.Models.Entities
{
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор объекта.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Возможные лоигны для аутентификации пользователя.
        /// </summary>
        [Required]
        public string UserName { get; private set; }

        public string EMail { get; private set; }

        public string PhoneNumber { get; private set; }


        /// <summary>
        /// Пароль от аккаунта.
        /// </summary>
        [Required]
        public string Password { get; private set; }

        /// <summary>
        /// Имя.
        /// </summary>
        [Required]
        public string Name { get; private set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        [Required]
        public string Sername { get; private set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        public string Fathername { get; private set; }
        
        public string Messangers { get; private set; }

        /// <summary>
        /// Роль пользователя в системе.
        /// </summary>
        [Required]
        public Role Priveleges { get; private set; }

        public User(Guid id, string userName, string eMail, string phoneNumber, string password, string name, string sername, string fathername, string messangers, Role priveleges)
        {
            Id = id;
            UserName = userName;
            EMail = eMail;
            PhoneNumber = phoneNumber;
            Password = PasswordHasherService.HashPassword(password);
            Name = name;
            Sername = sername;
            Fathername = fathername;
            Messangers = messangers;
            Priveleges = priveleges;
        }

        /// <summary>
        /// Приватный конструктор для EF Core
        /// </summary>
        private User()
        {
            Name = string.Empty;
            Password = string.Empty;
            Name = string.Empty;
            Sername = string.Empty;
            Priveleges = Role.NONE;
        }

        
    }
}
