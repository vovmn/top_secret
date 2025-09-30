using WSS.API.Domain.Enums;

namespace WSS.API.DTOs.Responses
{
    /// <summary>
    /// Ответ с полной информацией о графике работ.
    /// </summary>
    public class WorkScheduleResponse
    {
        public Guid Id { get; set; }
        public Guid ObjectId { get; set; }
        public WorkScheduleStatus Status { get; set; }
        public int Version { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<WorkItemResponse> WorkItems { get; set; } = new();
    }
}
