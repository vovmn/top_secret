using COM.API.Domain.Entities;
using COM.API.Infrastructure.Data;
using COM.API.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Checklist>> GetByConstructionObjectIdAsync(Guid constructionObjectId, CancellationToken cancellationToken = default)
        {
            return await _context.Checklists
                .Where(c => c.ConstructionObjectId == constructionObjectId)
                .ToListAsync(cancellationToken);
        }
    }
}
