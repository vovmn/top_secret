using WSS.API.Domain.Entities;

namespace WSS.API.Infrastructure.Interfaces
{
    public interface IWorkItemRepository
    {
        Task<WorkItem> AddAsync(WorkItem item);
        Task<WorkItem?> GetByIdAsync(Guid id);
        Task<List<WorkItem>> GetByScheduleIdAsync(Guid scheduleId);
        Task<List<WorkItem>> GetUnverifiedByObjectIdAsync(Guid objectId);
        Task<WorkItem> UpdateAsync(WorkItem item);
    }
}