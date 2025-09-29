using AIM.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AIM.Domain.ValueObjects
{
    public sealed partial record LoginInfo
    {
        /// <summary>
        /// Логин, если не указан, генерируется автоматически.
        /// </summary>
        [Required]
        public string Username { get; private set; }

        /// <summary>
        /// Email пользователя.
        /// </summary>
        public string? Email { get; private set; }

        /// <summary>
        /// Номер телефона пользователя.
        /// </summary>
        public string? PhoneNumber { get; private set; }

        /// <summary>
        /// Стандартный конструктор.
        /// Достаточно одного поля email или phoneNumber
        /// </summary>
        /// <exception cref="ArgumentException">Содержит информацию об ошибке</exception>
        public LoginInfo(string? username = null, string? email = null, string? phoneNumber = null)
        {
            if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Требуется указать Email или номер телефона");
            Username = string.IsNullOrWhiteSpace(username) ? GenerateLogin(email, phoneNumber) : ValidateLogin(username)!;
            Email = ValidateEmail(email);
            PhoneNumber = ValidatePhone(phoneNumber);
        }

        // Дальше идут 3 типовые функции для валидации

        /// <summary>
        /// Валидирует логин.
        /// </summary>
        private static string? ValidateLogin(string? login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return null;

            if (login.Length > 50)
                throw new ArgumentException($"Логин не может быть длиннее 50 символов");

            if (!RegexLoginCheck().IsMatch(login))
                throw new ArgumentException("Логин содержит недопустимые символы");

            return login.Trim();
        }

        /// <summary>
        /// Валидирует Email.
        /// </summary>
        private static string? ValidateEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            if (email.Length > 100)
                throw new ArgumentException($"Email не может быть длиннее 50 символов");

            if (!RegexEmailCheck().IsMatch(email))
                throw new ArgumentException("Некорректный формат Email");

            return email.Trim();
        }

        /// <summary>
        /// Валидирует номер телефона.
        /// СКОРЕЕ ВСЕГО ТРЕБУЕТ ДОРАБОТКИ
        /// </summary>
        private static string? ValidatePhone(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            value = RegexPhoneStripping().Replace(value, "");

            if (value.Length != 11 || !(value.StartsWith('7') || value.StartsWith('8')))
                throw new ArgumentException("Номер телефона должен быть в формате 7XXXXXXXXXX");

            return $"+7 ({value.Substring(1, 3)}) {value.Substring(4, 3)}-{value.Substring(7, 2)}-{value.Substring(9)}";
        }

        /// <summary>
        /// Генерирует логин на основе email или номера телефона.
        /// </summary>
        private static string GenerateLogin(string? email, string? phone)
        {
            string basePart = email?.Split('@')[0]?.Replace(".", "_")
                         ?? phone?.Substring(7, 4) // Последние 4 цифры номера
                         ?? throw new ArgumentException("Для генерации логина требуется email или телефон");

            string sanitized = RegexLoginSanitizer().Replace(basePart, "_");
            string timestamp = DateTime.UtcNow.ToString("HHmmss");

            return $"{sanitized}_{timestamp}";
        }

        /// <summary>
        /// Метод для проверки наличия какого-либо значения в классе.
        /// Например для проверки введённого пользователем логина на существование.
        /// </summary>
        public bool Contains(string? value)
        {
            return value != null && (value.Equals(Username, StringComparison.OrdinalIgnoreCase)
                || value.Equals(PhoneNumber, StringComparison.OrdinalIgnoreCase)
                || value.Equals(Email, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Парсинг лоигна на разрешённые символы (Utf, кирилица, _, -)
        /// </summary>
        [GeneratedRegex(@"^[a-zA-Z0-9_\-\.]+$")]
        private static partial Regex RegexLoginCheck();

        /// <summary>
        /// Парсинг email только на разрешённые символы.
        /// </summary>
        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, "ru-RU")]
        private static partial Regex RegexEmailCheck();

        /// <summary>
        /// Обрезает номер до циферек.
        /// </summary>
        [GeneratedRegex(@"\D")]
        private static partial Regex RegexPhoneStripping();

        /// <summary>
        /// Проверяет что логин содержит только допустимые символы.
        /// </summary>
        [GeneratedRegex(@"[^\w]")]
        private static partial Regex RegexLoginSanitizer();

        /// <summary>
        /// Конструктор для миграций.
        /// </summary>
        private LoginInfo() { }
    }
}
