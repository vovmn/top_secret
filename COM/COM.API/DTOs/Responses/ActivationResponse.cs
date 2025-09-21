namespace COM.API.DTOs.Responses
{
    /// <summary>
    /// DTO для ответа на запрос активации объекта.
    /// </summary>
    public class ActivationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid ActivatedObjectId { get; set; }
        public Guid ChecklistId { get; set; }
    }
}
