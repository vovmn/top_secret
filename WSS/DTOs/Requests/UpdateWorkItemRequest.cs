using WSS.API.Domain.Enums;

namespace WSS.API.DTOs.Requests
{
    /// <summary>
    /// Запрос на обновление вида работ (фактические даты, статус).
    /// Используется прорабом для отметки выполнения.
    /// </summary>
    public class UpdateWorkItemRequest
    {
        public Guid Id { get; set; }
        // === Поля для прораба (фактические данные) ===
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public WorkItemStatus Status { get; set; }

        // === Поля для СК (плановые данные) ===
        public DateTime? PlannedStartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
    }
}
