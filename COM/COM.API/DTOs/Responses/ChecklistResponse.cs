using COM.API.Domain.Enums;

namespace COM.API.DTOs.Responses
{
    /// <summary>
    /// DTO для ответа на запрос загрузки чек-листа.
    /// </summary>
    public class ChecklistResponse
    {
        public string Id { get; set; } = string.Empty;
        public string? FileId { get; set; }
        public ChecklistType Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
