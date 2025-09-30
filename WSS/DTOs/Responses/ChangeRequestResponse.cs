using WSS.API.Domain.Enums;

namespace WSS.API.DTOs.Responses
{
    /// <summary>
    /// Ответ с информацией о запросе на изменение графика.
    /// </summary>
    public class ChangeRequestResponse
    {
        public Guid Id { get; set; }
        public Guid WorkItemId { get; set; }
        public Guid RequestedBy { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateOnly NewStartDate { get; set; }
        public DateOnly NewEndDate { get; set; }
        public string Reason { get; set; } = string.Empty;
        public ChangeRequestStatus Status { get; set; }
        public Guid? ReviewedBy { get; set; }
        public DateTime? ReviewedAt { get; set; }
    }
}
