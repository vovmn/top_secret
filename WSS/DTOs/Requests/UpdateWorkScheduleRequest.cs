namespace WSS.API.DTOs.Requests
{
    public class UpdateWorkScheduleRequest
    {
        public List<UpdateWorkItemRequest> WorkItems { get; set; } = new();
    }

    /// <summary>
    /// Вспомогательный DTO для передачи Id + данных обновления.
    /// </summary>
    public class WorkItemUpdateEntry
    {
        public Guid Id { get; set; }
        public UpdateWorkItemRequest Data { get; set; } = new();
    }
}
