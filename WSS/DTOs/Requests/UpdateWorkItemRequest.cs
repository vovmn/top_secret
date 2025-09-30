using WSS.API.Domain.Enums;

namespace WSS.API.DTOs.Requests
{
    /// <summary>
    /// Запрос на обновление вида работ (фактические даты, статус).
    /// Используется прорабом для отметки выполнения.
    /// </summary>
    public class UpdateWorkItemRequest
    {
        /// <summary>
        /// Фактическая дата начала (может быть null).
        /// </summary>
        public DateOnly? ActualStartDate { get; set; }

        /// <summary>
        /// Фактическая дата окончания (может быть null).
        /// </summary>
        public DateOnly? ActualEndDate { get; set; }

        /// <summary>
        /// Новый статус работы.
        /// </summary>
        public WorkItemStatus Status { get; set; }
    }
}
