using COM.API.Domain.Enums;
using COM.API.Domain.ValueObjects;

namespace COM.API.DTOs.Responses
{
    /// <summary>
    /// DTO для возврата данных об объекте благоустройства.
    /// </summary>
    public class ObjectResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public ObjectStatus Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Coordinate> Polygon { get; set; } = [];
        public List<ResponsibleDto> Responsibles { get; set; } = [];
        public List<ChecklistDto> Checklists { get; set; } = [];
    }

    public class ResponsibleDto
    {
        public Guid UserId { get; set; }
        public ResponsibleRole Role { get; set; }
    }

    public class ChecklistDto
    {
        public string Id { get; set; } = string.Empty;
        public ChecklistType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? FileId { get; set; }
    }
}
