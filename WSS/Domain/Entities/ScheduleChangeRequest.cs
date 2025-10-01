using WSS.API.Domain.Enums;

namespace WSS.API.Domain.Entities
{
    /// <summary>
    /// Запрос на изменение сроков выполнения работ в графике.
    /// Инициируется прорабом, согласуется службой контроля.
    /// </summary>
    public class ScheduleChangeRequest
    {
        /// <summary>
        /// Уникальный идентификатор запроса.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ссылка на график.
        /// </summary>
        public Guid ScheduleId { get; set; }

        /// <summary>
        /// Ссылка на конкретную работу (WorkItem), которую хотят изменить.
        /// </summary>
        public Guid WorkItemId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, инициировавшего запрос (обычно прораб).
        /// </summary>
        public Guid RequestedBy { get; set; }

        /// <summary>
        /// Дата создания запроса.
        /// </summary>
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Новые предлагаемые даты начала и окончания.
        /// </summary>
        public DateTime NewStartDate { get; set; }
        public DateTime NewEndDate { get; set; }

        /// <summary>
        /// Обоснование изменения (например, "поставка материала задержана").
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// Статус запроса.
        /// </summary>
        public ChangeRequestStatus Status { get; set; } = ChangeRequestStatus.Pending;

        /// <summary>
        /// Кто рассмотрел запрос (сотрудник службы контроля).
        /// </summary>
        public Guid? ReviewedBy { get; set; }

        /// <summary>
        /// Когда был рассмотрен запрос.
        /// </summary>
        public DateTime? ReviewedAt { get; set; }
    }
}
