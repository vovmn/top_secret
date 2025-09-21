namespace COM.API.DTOs.Requests
{
    /// <summary>
    /// DTO для активации объекта благоустройства.
    /// Содержит данные, необходимые для назначения ответственных и загрузки акта.
    /// </summary>
    public class ActivateObjectRequest
    {
        public Guid ObjectId { get; set; }
        public Guid ForemanUserId { get; set; }
        public Guid InspectorSKUserId { get; set; }
        public Guid InspectorKOUserId { get; set; }
        public double UserLatitude { get; set; } // Для проверки геопозиции
        public double UserLongitude { get; set; }
        public Stream? ActFile { get; set; } // Файл акта открытия
        public string? ActFileName { get; set; }
        public string? ChecklistAnswers { get; set; } // JSON-строка с ответами на вопросы чек-листа
    }
}
