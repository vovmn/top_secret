using Microsoft.EntityFrameworkCore;
using WSS.API.Domain.Entities;
using WSS.API.Domain.Enums;
using WSS.API.Infrastructure.Data;
using WSS.API.Infrastructure.Interfaces;

namespace WSS.API.Infrastructure.Repositories
{
    /// <summary>
    /// Реализация репозитория запросов на изменение графика.
    /// </summary>
    public class ChangeRequestRepository(ApplicationDbContext context) : IChangeRequestRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<ScheduleChangeRequest> CreateAsync(ScheduleChangeRequest request)
        {
            _context.ChangeRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<ScheduleChangeRequest?> GetByIdAsync(Guid id)
        {
            return await _context.ChangeRequests.FindAsync(id);
        }

        public async Task<List<ScheduleChangeRequest>> GetPendingByObjectIdAsync(Guid objectId)
        {
            var scheduleId = await _context.WorkSchedules
                .Where(s => s.ObjectId == objectId)
                .Select(s => s.Id)
                .FirstOrDefaultAsync();

            if (scheduleId == Guid.Empty) return new List<ScheduleChangeRequest>();

            return await _context.ChangeRequests
                .Where(r => r.ScheduleId == scheduleId && r.Status == ChangeRequestStatus.Pending)
                .ToListAsync();
        }

        public async Task<ScheduleChangeRequest> UpdateStatusAsync(
        Guid requestId,
        ChangeRequestStatus newStatus,
        Guid reviewedBy)
        {
            var request = await _context.ChangeRequests.FindAsync(requestId);
            if (request == null)
                throw new KeyNotFoundException("Запрос не найден.");

            request.Status = newStatus;
            request.ReviewedBy = reviewedBy;
            request.ReviewedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return request;
        }
    }
}
