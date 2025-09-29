using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

// CHATGPT or some stackpverflow bullshit

namespace AIM.Core.Exceptions
{
    [Serializable]
    public class RepositoryException : Exception
    {
        public string EntityType { get; }
        public Guid? EntityId { get; }

        // Базовый конструктор
        public RepositoryException(string message)
        : base(message) { }

        // Конструктор с внутренним исключением
        public RepositoryException(string message, Exception innerException)
        : base(message, innerException) { }

        // Конструктор с доп. параметрами
        public RepositoryException(
        string message,
        string entityType,
        Guid? entityId = null,
        Exception innerException = null)
        : base(message, innerException)
        {
            EntityType = entityType;
            EntityId = entityId;
        }

        // Для сериализации
        protected RepositoryException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
        {
            EntityType = info.GetString(nameof(EntityType));
            EntityId = (Guid?)info.GetValue(nameof(EntityId), typeof(Guid?));
        }

        public override void GetObjectData(
        SerializationInfo info,
        StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(EntityType), EntityType);
            info.AddValue(nameof(EntityId), EntityId);
        }
    }
}
