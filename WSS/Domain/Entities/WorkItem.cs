using WSS.API.Domain.Enums;

namespace WSS.API.Domain.Entities
{
    /// <summary>
    /// Элемент сетевого графика — отдельный вид работ (строка спецификации).
    /// Пример: "Укладка брусчатки", "Озеленение", "Монтаж малых архитектурных форм".
    /// </summary>
    public class WorkItem
    {
        /// <summary>
        /// Уникальный идентификатор работы.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ссылка на график, к которому относится работа.
        /// </summary>
        public Guid ScheduleId { get; set; }

        /// <summary>
        /// Наименование вида работ.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Плановая дата начала выполнения.
        /// </summary>
        public DateOnly PlannedStartDate { get; set; }

        /// <summary>
        /// Плановая дата окончания выполнения.
        /// </summary>
        public DateOnly PlannedEndDate { get; set; }

        /// <summary>
        /// Фактическая дата начала (заполняется прорабом).
        /// </summary>
        public DateOnly? ActualStartDate { get; set; }

        /// <summary>
        /// Фактическая дата окончания (заполняется прорабом).
        /// </summary>
        public DateOnly? ActualEndDate { get; set; }

        /// <summary>
        /// Статус выполнения работы.
        /// </summary>
        public WorkItemStatus Status { get; set; } = WorkItemStatus.Planned;

        /// <summary>
        /// Идентификатор прораба (подрядчика), ответственного за выполнение.
        /// </summary>
        public Guid? ResponsibleContractorId { get; set; }

        /// <summary>
        /// Идентификатор сотрудника службы строительного контроля, подтвердившего выполнение.
        /// </summary>
        public Guid? VerifiedByControlId { get; set; }

        /// <summary>
        /// Дата и время подтверждения выполнения.
        /// </summary>
        public DateTime? VerifiedAt { get; set; }
    }
}
