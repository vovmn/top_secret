using System.ComponentModel.DataAnnotations;

namespace AIM.Domain.Entities
{
    public sealed class RefreshToken
    {
        /// <summary>
        /// Уникальный идентификатор объекта.
        /// </summary>
        [Required]
        public Guid Token { get; private set; }

        /// <summary>
        /// Дата окончания работы токена.
        /// </summary>
        [Required]
        public DateTime Expires { get; private set; }

        /// <summary>
        /// Ссылка на пользователя.
        /// </summary>
        [Required]
        public Guid UserId { get; private set; }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <param name="expires"></param>
        /// <param name="userId"></param>
        public RefreshToken(Guid token, DateTime expires, Guid userId)
        {
            if (expires <= DateTime.UtcNow)
                throw new ArgumentException("Expiration must be in future");

            Token = token;
            Expires = expires;
            UserId = userId;
        }

        public bool Validate() => Expires <= DateTime.UtcNow;

        /// <summary>
        /// Конструктор для миграций.
        /// </summary>
        private RefreshToken() { }

    }
}
