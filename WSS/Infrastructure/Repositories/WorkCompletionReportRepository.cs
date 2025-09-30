using WSS.API.Domain.Entities;
using WSS.API.Infrastructure.Data;
using WSS.API.Infrastructure.Interfaces;

namespace WSS.API.Infrastructure.Repositories
{
    /// <summary>
    /// Реализация репозитория отчётов о завершении работ.
    /// </summary>
    public class WorkCompletionReportRepository(ApplicationDbContext context) : IWorkCompletionReportRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<WorkCompletionReport> SaveAsync(WorkCompletionReport report)
        {
            _context.CompletionReports.Update(report); // Upsert
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<WorkCompletionReport?> GetByWorkItemIdAsync(Guid workItemId)
        {
            return await _context.CompletionReports.FindAsync(workItemId);
        }

        public async Task VerifyAsync(Guid workItemId)
        {
            var report = await _context.CompletionReports.FindAsync(workItemId);
            if (report != null)
            {
                report.IsVerified = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
