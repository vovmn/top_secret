using WSS.API.Domain.Enums;

namespace WSS.API.DTOs.Responses
{
    /// <summary>
    /// Ответ с информацией о виде работ.
    /// </summary>
    public class WorkItemResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly PlannedStartDate { get; set; }
        public DateOnly PlannedEndDate { get; set; }
        public DateOnly? ActualStartDate { get; set; }
        public DateOnly? ActualEndDate { get; set; }
        public WorkItemStatus Status { get; set; }
        public Guid? ResponsibleContractorId { get; set; }
        public Guid? VerifiedByControlId { get; set; }
        public DateTime? VerifiedAt { get; set; }
    }
}
