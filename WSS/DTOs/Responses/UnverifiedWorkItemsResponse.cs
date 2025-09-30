namespace WSS.API.DTOs.Responses
{
    /// <summary>
    /// Ответ со списком неподтверждённых работ для службы контроля.
    /// </summary>
    public class UnverifiedWorkItemsResponse
    {
        public Guid ObjectId { get; set; }
        public List<WorkItemResponse> Items { get; set; } = new();
    }
}
