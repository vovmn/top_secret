using WSS.API.Domain.Enums;

namespace WSS.API.Domain.Entities
{
    /// <summary>
    /// Отчёт о завершении работ, отправляемый прорабом.
    /// Содержит фото, комментарии и метаданные (геопозиция, время).
    /// </summary>
    public class WorkCompletionReport
    {
        /// <summary>
        /// Ссылка на работу, по которой подаётся отчёт.
        /// </summary>
        public Guid WorkItemId { get; set; }

        /// <summary>
        /// Кто отправил отчёт (прораб).
        /// </summary>
        public Guid ReportedBy { get; set; }

        /// <summary>
        /// Время отправки отчёта.
        /// </summary>
        public DateTime ReportedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Географическая широта места отправки (фиксация посещения объекта).
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Географическая долгота места отправки.
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Комментарий прораба (опционально).
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// Список идентификаторов прикреплённых фотографий (хранятся в Document Storage Service).
        /// </summary>
        public List<string> PhotoDocumentIds { get; set; } = new();

        /// <summary>
        /// Признак того, что отчёт был подтверждён службой контроля.
        /// </summary>
        public bool IsVerified { get; set; } = false;
    }
}
