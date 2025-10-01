using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace IAM.Domain.ValueObjects
{
    /// <summary>
    /// Содержит информацию о ФИО пользователяю.
    /// </summary>
    public sealed partial record FIO
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required]
        public string Name { get; private set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        [Required]
        public string Sername { get; private set; }

        /// <summary>
        /// Отчество пользователя. (При наличии)
        /// </summary>
        public string? Fathername { get; private set; }

        /// <summary>
        /// Основной конструктор
        /// </summary>
        public FIO(string? name, string? sername, string? fathername = null)
        {
            Name = ValidateField(name, "Имя")!;
            Sername = ValidateField(sername, "Фамилия")!;
            Fathername = ValidateField(fathername, "Отчество");
        }

        /// <summary>
        /// Валидирует отправленные поля
        /// </summary>
        /// <param name="value">Проверяемое значение.</param>
        /// <param name="fieldName">Проверяемое поле.</param>
        /// <returns>Возвращает валидированное значение.</returns>
        /// <exception cref="ArgumentException">Указывает допущенную ошибку при валидации.</exception>
        private static string? ValidateField(string? value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value) && fieldName == "Отчество")
                return null;

            if (string.IsNullOrWhiteSpace(value))
                throw new ValidationException($"{fieldName} не может быть пустым");

            if (value.Length > 50)
                throw new ValidationException($"{fieldName} не может быть длиннее 50 символов");

            if (!RegexFIOCheck().IsMatch(value))
                throw new ValidationException($"{fieldName} содержит недопустимые символы");

            return value.Trim();
        }


        /// <summary>
        /// Полное ФИО (если нет отчества, то просто фамилия и имя)
        /// </summary>
        public string FullName => Fathername != null? $"{Sername} {Name} {Fathername}" : $"{Sername} {Name}";

        /// <summary>
        /// Инициалы (если нет отчества, то без него)
        /// Формат -> Фамилия И.О.
        /// </summary>
        public string Initials => Fathername != null ? $"{Sername} {Name[0]}.{Fathername[0]}." : $"{Sername} {Name[0]}.";

        /// <summary>
        /// Парсинг из строки (например, "Иванов Иван Иванович")
        /// </summary>
        public static FIO Parse(string fullName)
        {
            string[] parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return parts.Length switch
            {
                2 => new FIO(parts[0], parts[1]),
                3 => new FIO(parts[0], parts[1], parts[2]),
                _ => throw new FormatException("Некорректный формат ФИО")
            };
        }

        /// <summary>
        /// Неявный метод для более простого преобразования
        /// </summary>
        public override string ToString() => FullName;

        /// <summary>
        /// Парсинг строк на разрешённые символы (Utf, кирилица, ', -)
        /// </summary>
        [GeneratedRegex(@"^[\p{L}\-'\s]+$")]
        private static partial Regex RegexFIOCheck();

        /// <summary>
        /// Конструктор для миграций.
        /// </summary>
        private FIO() {}
    }
}
