using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AIM.Domain.ValueObjects
{
    /// <summary>
    /// Класс хранящий в себе информацию пользователей в качестве контактной информации
    /// Требует дальнейшей доработки
    /// </summary>
    public sealed partial record Messengers
    {
        /// <summary>
        /// Номер пользователя для WhatsApp
        /// </summary>
        public string? WhatsApp { get; set; }

        /// <summary>
        /// Ссылка на страницу пользователя в вк
        /// </summary>
        public string? VK { get; set; }

        /// <summary>
        /// Индентификатор пользователя в том что ловит даже на парковке
        /// (Газпром, занесите нам пж деняг, мы тогда вам тут нормальный метод валидации пропишем)
        /// ((Я даже max скачаю))
        /// </summary>
        public string? Max { get; set; }

        /// <summary>
        /// Имя пользователя в телеге
        /// </summary>
        public string? Telegram { get; set; }

        /// <summary>
        /// Любая другая форма записи контактной информации
        /// </summary>
        public string? Other { get; set; }

        /// <summary>
        /// Основной конструктор, внесение данных чисто по желанию, но можно и сделать 1 из них обязательным.
        /// </summary>
        public Messengers(string? whatsApp = null, string? vK = null, string? max = null, string? telegram = null, string? other = null)
        {
            WhatsApp = ValidateWhatsApp(whatsApp);
            VK = ValidateVK(vK);
            Max = ValidateMax(max);
            Telegram = ValidateTelegram(telegram);
            Other = OtherValidator(other);
        }

        // Специализированные валидаторы
        private static string? ValidateWhatsApp(string? number)
        {
            if (string.IsNullOrWhiteSpace(number))
                return null;
            if (!RegexWhatsAppCheck().IsMatch(number))
                throw new ArgumentException("Некорректный формат WhatsApp");
            return number.StartsWith('+') ? number : $"+{number}";
        }

        private static string? ValidateVK(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return null;
            if (!RegexVKCheck().IsMatch(url))
                throw new ArgumentException("Некорректная страница VK");
            return url.ToLowerInvariant();
        }

        private static string? ValidateMax(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            if (value.Length < 5 || value.Length > 50)
                throw new ArgumentException("Идентификатор Max должен быть от 5 до 50 символов");
            return value.Trim();
        }

        private static string? ValidateTelegram(string? username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;
            if (!RegexTelegramCheck().IsMatch(username))
                throw new ArgumentException("Некорректный Telegram username");

            return username.ToLowerInvariant();
        }

        private static string? OtherValidator(string? value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 100)
                throw new ArgumentException("Название мессенджера должно быть от 1 до 100 символов");

            return value.Trim();
        }

        // Регексы для обработки данных
        [GeneratedRegex(@"^\+?[1-9]\d{7,14}$")]
        private static partial Regex RegexWhatsAppCheck();
        [GeneratedRegex(@"^https?:\/\/(vk\.com|vkontakte\.ru)\/[a-zA-Z0-9_\.]+$", RegexOptions.IgnoreCase, "ru-RU")]
        private static partial Regex RegexVKCheck();
        [GeneratedRegex(@"^@[a-zA-Z0-9_]{5,32}$")]
        private static partial Regex RegexTelegramCheck();

        /// <summary>
        /// Конструктор для миграций.
        /// </summary>
        private Messengers() {}
    }
}
