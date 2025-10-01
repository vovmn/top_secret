using WSS.API.DTOs.Requests;
using WSS.API.DTOs.Responses;

namespace WSS.API.Services
{
    public interface IWorkScheduleService
    {
        Task<WorkScheduleResponse> GetScheduleByObjectIdAsync(Guid objectId);
        Task InitializeScheduleAsync(Guid objectId, List<CreateWorkItemRequest> workItems);
        Task MarkWorkAsCompletedAsync(Guid workItemId, UpdateWorkItemRequest updateRequest, SubmitCompletionReportRequest reportRequest, Guid reportedBy);
        Task UpdateScheduleDirectlyAsync(Guid objectId, List<UpdateWorkItemRequest> updates, Guid updatedBy);
    }
}