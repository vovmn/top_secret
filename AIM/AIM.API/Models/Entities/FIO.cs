using System.ComponentModel.DataAnnotations;

namespace AIM.API.Models.Entities
{
    /// <summary>
    /// Содержит информацию о ФИО пользователяю.
    /// </summary>
    public class FIO(Guid userId, string name, string sername, string? fathername)
    {

        /// <summary>
        /// Id пользователя (внешний ключ)
        /// </summary>
        public Guid UserId { get; set; } = userId;

        /// <summary>
        /// Имя.
        /// </summary>
        [Required]
        public string Name { get; private set; } = name;

        /// <summary>
        /// Фамилия.
        /// </summary>
        [Required]
        public string Sername { get; private set; } = sername;

        /// <summary>
        /// Отчество.
        /// </summary>
        public string? Fathername { get; private set; } = fathername;

        /// <summary>
        /// Полное ФИО (если нет отчества, то просто фамилия и имя)
        /// </summary>
        public string FullName => $"{Sername} {Name}" + (string.IsNullOrWhiteSpace(Fathername) ? "" : $" {Fathername}");

    }
}
