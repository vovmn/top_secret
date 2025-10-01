using IAM.Domain.Enums;
using IAM.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IAM.Domain.Entities
{
    public sealed class User
    {
        /// <summary>
        /// Уникальный идентификатор объекта.
        /// </summary>
        [Required]
        public Guid Id { get; private set; }

        /// <summary>
        /// Возможные логины для аутентификации пользователя.
        /// </summary>
        public LoginInfo LoginInfo { get; private set; }

        /// <summary>
        /// Пароль от аккаунта.
        /// </summary>
        [Required]
        public string Password { get; private set; }

        /// <summary>
        /// Фио пользователя
        /// </summary>
        public FIO FIO { get; private set; }

        /// <summary>
        /// Мессенджеры для контакта с пользователем (ПОТОМ ПРОДАДИМ! :3)
        /// </summary>
        public Messengers Messengers { get; private set; }

        /// <summary>
        /// Роль пользователя в системе.
        /// </summary>
        [Required]
        public Roles Priveleges { get; private set; }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        /// <param name="id">Id пользователя.</param>
        /// <param name="loginInfo">Информация для аунтентификации пользователя.</param>
        /// <param name="password">Хешированный пароль пользователя.</param>
        /// <param name="fio">ФИО пользователя.</param>
        /// <param name="messangers">Ссылки для контакта с пользователем.</param>
        /// <param name="priveleges">Роль пользователя.</param>
        /// <exception cref="ArgumentNullException">Возникает если необходимое поле не найдено в бд.</exception>
        public User(Guid id, LoginInfo loginInfo, string password, FIO fio, Messengers messangers, Roles priveleges)
        {
            Id = id;
            LoginInfo = loginInfo ?? throw new ArgumentNullException(nameof(loginInfo), "Сервер не смог собрать данные о логине пользователя");
            Password = password;
            FIO = fio ?? throw new ArgumentNullException(nameof(fio), "Сервер не смог собрать данные о ФИО пользователя");
            Messengers = messangers ?? throw new ArgumentNullException(nameof(messangers), "Сервер не смог собрать данные о мессенджерах пользователя");
            Priveleges = ValidateRole(priveleges);
        }

        /// <summary>
        /// Проверяет, что назваченная роль существует.
        /// </summary>
        private static Roles ValidateRole(Roles role)
        {
            if (!Enum.IsDefined(typeof(Roles), role))
                throw new ValidationException("Недопустимая роль пользователя");

            return role;
        }

        // Методы для изменения полей.
        public User UpdateLoginInfo(LoginInfo newLoginInfo)
        {
            return new User(
                Id,
                newLoginInfo,
                Password,
                FIO,
                Messengers,
                Priveleges
            );
        }

        public User UpdatePassword(string newPassword)
        {
            return new User(
                Id,
                LoginInfo,
                newPassword,
                FIO,
                Messengers,
                Priveleges
            );
        }

        public User UpdateFIO(FIO newFio)
        {
            return new User(
                Id,
                LoginInfo,
                Password,
                newFio,
                Messengers,
                Priveleges
            );
        }

        public User UpdateMessengers(Messengers newMessengers)
        {
            return new User(
                Id,
                LoginInfo,
                Password,
                FIO,
                newMessengers,
                Priveleges
            );
        }

        public User UpdatePriveleges(Roles newPriveleges)
        {
            return new User(
                Id,
                LoginInfo,
                Password,
                FIO,
                Messengers,
                newPriveleges
            );
        }

        /// <summary>
        /// Конструктор для миграций
        /// </summary>
        private User() { }
    }
}
