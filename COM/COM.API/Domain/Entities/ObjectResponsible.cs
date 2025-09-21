using COM.API.Domain.Enums;

namespace COM.API.Domain.Entities
{
    public class ObjectResponsible
    {
        public Guid ConstructionObjectId { get; private set; } // FK к ConstructionObject
        public Guid UserId { get; private set; } // ID пользователя из IAM Service
        public ResponsibleRole Role { get; private set; } // Роль пользователя на этом объекте

        public ObjectResponsible(Guid objectId, Guid userId, ResponsibleRole role)
        {
            ConstructionObjectId = objectId;
            UserId = userId;
            Role = role;
        }

        // Приватный конструктор для EF Core
        private ObjectResponsible() { }
    }
}
