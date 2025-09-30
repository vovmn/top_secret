using WSS.API.Infrastructure.Data;
using WSS.API.Infrastructure.Interfaces;

namespace WSS.API.Infrastructure.Repositories
{
    /// <summary>
    /// Реализация Unit of Work на основе EF Core DbContext.
    /// Обеспечивает единый контекст и транзакцию для всех репозиториев.
    /// </summary>
    public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = context;
        private IWorkScheduleRepository? _workSchedules;
        private IWorkItemRepository? _workItems;
        private IChangeRequestRepository? _changeRequests;
        private IWorkCompletionReportRepository? _completionReports;

        public IWorkScheduleRepository WorkSchedules =>
            _workSchedules ??= new WorkScheduleRepository(_context);

        public IWorkItemRepository WorkItems =>
        _workItems ??= new WorkItemRepository(_context);

        public IChangeRequestRepository ChangeRequests =>
            _changeRequests ??= new ChangeRequestRepository(_context);

        public IWorkCompletionReportRepository CompletionReports =>
            _completionReports ??= new WorkCompletionReportRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
