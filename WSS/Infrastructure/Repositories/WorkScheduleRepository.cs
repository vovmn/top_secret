using Microsoft.EntityFrameworkCore;
using WSS.API.Domain.Entities;
using WSS.API.Domain.Enums;
using WSS.API.Infrastructure.Data;
using WSS.API.Infrastructure.Interfaces;

namespace WSS.API.Infrastructure.Repositories
{
    /// <summary>
    /// Реализация репозитория сетевых графиков с использованием EF Core.
    /// </summary>
    public class WorkScheduleRepository(ApplicationDbContext context) : IWorkScheduleRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<WorkSchedule> CreateAsync(WorkSchedule schedule)
        {
            _context.WorkSchedules.Add(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task<WorkSchedule?> GetByObjectIdAsync(Guid objectId)
        {
            return await _context.WorkSchedules
                .Include(s => s.WorkItems)
                .FirstOrDefaultAsync(s => s.ObjectId == objectId);
        }

        public async Task<WorkSchedule?> GetByIdAsync(Guid id)
        {
            return await _context.WorkSchedules
                .Include(s => s.WorkItems)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<WorkSchedule> UpdateAsync(WorkSchedule schedule)
        {
            // Оптимистичная блокировка через Version
            var existing = await _context.WorkSchedules.FindAsync(schedule.Id);
            if (existing == null || existing.Version != schedule.Version - 1)
                throw new InvalidOperationException("График был изменён другим пользователем. Повторите попытку.");

            _context.Entry(existing).CurrentValues.SetValues(schedule);
            existing.Version++;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task ArchiveAsync(Guid scheduleId)
        {
            var schedule = await _context.WorkSchedules.FindAsync(scheduleId);
            if (schedule != null)
            {
                schedule.Status = WorkScheduleStatus.Archived;
                await _context.SaveChangesAsync();
            }
        }
    }
}
