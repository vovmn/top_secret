using WSS.API.Domain.Entities;
using WSS.API.Domain.Enums;

namespace WSS.API.Infrastructure.Interfaces
{
    public interface IChangeRequestRepository
    {
        Task<ScheduleChangeRequest> CreateAsync(ScheduleChangeRequest request);
        Task<ScheduleChangeRequest?> GetByIdAsync(Guid id);
        Task<List<ScheduleChangeRequest>> GetPendingByObjectIdAsync(Guid objectId);
        Task<ScheduleChangeRequest> UpdateStatusAsync(Guid requestId, ChangeRequestStatus newStatus, Guid reviewedBy);
    }
}