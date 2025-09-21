using COM.API.Domain.Enums;

namespace COM.API.DTOs.Requests
{
    /// <summary>
    /// DTO для загрузки чек-листа.
    /// </summary>
    public class UploadChecklistRequest
    {
        public Guid ConstructionObjectId { get; set; }
        public Stream? File { get; set; }
        public string? FileName { get; set; }
        public ChecklistType Type { get; set; }
        public string? Content { get; set; } // JSON-содержимое
    }
}
