using Microsoft.EntityFrameworkCore;
using WSS.API.Domain.Entities;
using WSS.API.Domain.Enums;
using WSS.API.Infrastructure.Data;
using WSS.API.Infrastructure.Interfaces;

namespace WSS.API.Infrastructure.Repositories
{
    /// <summary>
    /// Реализация репозитория видов работ.
    /// </summary>
    public class WorkItemRepository(ApplicationDbContext context) : IWorkItemRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<WorkItem> AddAsync(WorkItem item)
        {
            _context.WorkItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<WorkItem> UpdateAsync(WorkItem item)
        {
            _context.WorkItems.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<List<WorkItem>> GetByScheduleIdAsync(Guid scheduleId)
        {
            return await _context.WorkItems
                .Where(w => w.ScheduleId == scheduleId)
                .ToListAsync();
        }

        public async Task<WorkItem?> GetByIdAsync(Guid id)
        {
            return await _context.WorkItems.FindAsync(id);
        }

        public async Task<List<WorkItem>> GetUnverifiedByObjectIdAsync(Guid objectId)
        {
            return await _context.WorkItems
                .Where(w => w.ScheduleId == _context.WorkSchedules
                    .Where(s => s.ObjectId == objectId)
                    .Select(s => s.Id)
                    .FirstOrDefault()
                    && w.Status == WorkItemStatus.Completed)
                .ToListAsync();
        }
    }
}
