namespace AIM.Application.Services
{
    /// <summary>
    /// Хэширование паролей
    /// </summary>
    public static class PasswordHasherService
    {
        // Константа для определения количества раундов хеширования
        private const int WorkFactor = 12;

        /// <summary>
        /// Создает хеш пароля
        /// </summary>
        /// <param name="password">Пароль для хеширования</param>
        /// <returns>Хеш пароля</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Введите пароль.");

            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
        }

        /// <summary>
        /// Проверяет соответствие пароля хешу
        /// </summary>
        /// <param name="password">Пароль для проверки</param>
        /// <param name="hash">Хеш для сравнения</param>
        /// <returns>true, если пароль соответствует хешу</returns>
        public static bool VerifyPassword(string password, string hash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash))
                throw new ArgumentException("Неверный пароль.");

            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
