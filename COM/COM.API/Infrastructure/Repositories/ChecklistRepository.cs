using COM.API.Domain.Entities;
using COM.API.Infrastructure.Data;
using COM.API.Infrastructure.Interfaces;

namespace COM.API.Infrastructure.Repositories
{
    /// <summary>
    /// Реализация репозитория для сущности Checklist.
    /// </summary>
    public class ChecklistRepository(ApplicationDbContext context) : IChecklistRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddAsync(Checklist checklist, CancellationToken cancellationToken = default)
        {
            await _context.Checklists.AddAsync(checklist, cancellationToken);
        }
    }
}
