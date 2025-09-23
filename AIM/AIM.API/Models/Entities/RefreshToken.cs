using System.ComponentModel.DataAnnotations;

namespace AIM.API.Models.Entities
{
    public class RefreshToken
    {

        /// <summary>
        /// Уникальный идентификатор объекта.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Токен для авторизации.
        /// </summary>
        [Required]
        public string Token { get; set; } = null!;

        /// <summary>
        /// Дата окончания работы токена
        /// </summary>
        [Required]
        public DateTime Expires { get; set; }

        public Guid UserId { get; set; }

    }
}
