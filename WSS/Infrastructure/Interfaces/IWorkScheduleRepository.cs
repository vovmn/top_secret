using WSS.API.Domain.Entities;

namespace WSS.API.Infrastructure.Interfaces
{
    public interface IWorkScheduleRepository
    {
        Task ArchiveAsync(Guid scheduleId);
        Task<WorkSchedule> CreateAsync(WorkSchedule schedule);
        Task<WorkSchedule?> GetByIdAsync(Guid id);
        Task<WorkSchedule?> GetByObjectIdAsync(Guid objectId);
        Task<WorkSchedule> UpdateAsync(WorkSchedule schedule);
    }
}