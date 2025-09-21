namespace COM.API.Domain.Events
{
    /// <summary>
    /// Доменное событие, публикуемое при успешной активации объекта благоустройства.
    /// Используется для информирования других частей системы (или других микросервисов) о том,
    /// что объект готов к работе. Например, может использоваться для автоматического создания
    /// задачи в Work Schedule Service или для уведомления инспектора КО.
    /// </summary>
    public class ObjectActivatedEvent(
        Guid objectId,
        DateTime activationDate,
        Guid foremanUserId,
        Guid inspectorSKUserId,
        Guid inspectorKOUserId)
    {
        /// <summary>
        /// ID активированного объекта.
        /// </summary>
        public Guid ObjectId { get; } = objectId;

        /// <summary>
        /// Дата и время активации.
        /// </summary>
        public DateTime ActivationDate { get; } = activationDate;

        /// <summary>
        /// ID пользователя, назначенного прорабом.
        /// </summary>
        public Guid ForemanUserId { get; } = foremanUserId;

        /// <summary>
        /// ID пользователя, назначенного инспектором службы контроля (заказчик).
        /// </summary>
        public Guid InspectorSKUserId { get; } = inspectorSKUserId;

        /// <summary>
        /// ID пользователя, назначенного инспектором контрольного органа.
        /// </summary>
        public Guid InspectorKOUserId { get; } = inspectorKOUserId;
    }
}
