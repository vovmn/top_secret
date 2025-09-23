using COM.API.Infrastructure.Data;
using COM.API.Infrastructure.Interfaces;

namespace COM.API.Infrastructure.Repositories
{
    /// <summary>
    /// Реализация паттерна Unit of Work на основе Entity Framework Core.
    /// Управляет жизненным циклом DbContext и обеспечивает, что все репозитории используют один и тот же контекст.
    /// Сохранение изменений происходит атомарно для всех репозиториев.
    /// </summary>
    public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = context;
        private IObjectRepository? _objects;
        private IChecklistRepository? _checklists;

        public IObjectRepository Objects => _objects ??= new ObjectRepository(_context);

        public IChecklistRepository Checklists => _checklists ??= new ChecklistRepository(_context);

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
