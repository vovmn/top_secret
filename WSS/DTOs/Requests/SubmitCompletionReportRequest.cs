namespace WSS.API.DTOs.Requests
{
    /// <summary>
    /// Запрос на отправку отчёта о завершении работ.
    /// Содержит геопозицию и документы.
    /// </summary>
    public class SubmitCompletionReportRequest
    {
        public string? PhotoFileId { get; set; }
        public string? Comment { get; set; }

        /// <summary>
        /// Широта местоположения при отправке (обязательна).
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Долгота местоположения при отправке (обязательна).
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Комментарий прораба (опционально).
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// Идентификаторы загруженных фото (из Document Storage Service).
        /// </summary>
        public List<string> PhotoDocumentIds { get; set; } = new();
    }
}
