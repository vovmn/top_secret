using COM.API.Domain.Enums;

namespace COM.API.Domain.Entities
{
    /// <summary>
    /// Сущность, представляющая связь между объектом благоустройства и ответственным за него пользователем.
    /// Отражает требования ТЗ по назначению ролей: Прораб (подрядчик), Инспектор СК (заказчик), Инспектор КО.
    /// </summary>
    public class ObjectResponsible
    {
        /// <summary>
        /// ID объекта благоустройства, к которому привязан пользователь.
        /// </summary>
        public Guid ConstructionObjectId { get; private set; }

        /// <summary>
        /// ID пользователя (из IAM Service), назначенного на объект.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Роль пользователя на данном объекте.
        /// </summary>
        public ResponsibleRole Role { get; private set; } // Роль пользователя на этом объекте


        /// <summary>
        /// Конструктор для создания связи "объект-пользователь-роль".
        /// </summary>
        /// <param name="objectId">ID объекта благоустройства.</param>
        /// <param name="userId">ID пользователя.</param>
        /// <param name="role">Роль пользователя на объекте.</param>
        public ObjectResponsible(Guid objectId, Guid userId, ResponsibleRole role)
        {
            ConstructionObjectId = objectId;
            UserId = userId;
            Role = role;
        }

        /// <summary>
        /// Приватный конструктор для EF Core
        /// </summary>
        private ObjectResponsible() { }
    }
}
