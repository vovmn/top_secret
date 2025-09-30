using WSS.API.Domain.Entities;

namespace WSS.API.Infrastructure.Interfaces
{
    public interface IWorkCompletionReportRepository
    {
        Task<WorkCompletionReport?> GetByWorkItemIdAsync(Guid workItemId);
        Task<WorkCompletionReport> SaveAsync(WorkCompletionReport report);
        Task VerifyAsync(Guid workItemId);
    }
}