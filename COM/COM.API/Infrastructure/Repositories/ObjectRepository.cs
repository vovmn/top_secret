using COM.API.Domain.Entities;
using COM.API.Infrastructure.Data;
using COM.API.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace COM.API.Infrastructure.Repositories
{
    /// <summary>
    /// Реализация репозитория для сущности ConstructionObject.
    /// Выполняет операции CRUD с использованием Entity Framework Core.
    /// </summary>
    public class ObjectRepository(ApplicationDbContext context) : IObjectRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<ConstructionObject?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.ConstructionObjects
                .Include(co => co.Responsibles) // Загружаем связанных ответственных
                .Include(co => co.Checklists)   // Загружаем чек-листы
                .FirstOrDefaultAsync(co => co.Id == id, cancellationToken);
        }

        public async Task AddAsync(ConstructionObject constructionObject, CancellationToken cancellationToken = default)
        {
            await _context.ConstructionObjects.AddAsync(constructionObject, cancellationToken);
        }

        public async Task UpdateAsync(ConstructionObject constructionObject, CancellationToken cancellationToken = default)
        {
            _context.ConstructionObjects.Update(constructionObject);
            await Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.ConstructionObjects.AnyAsync(co => co.Id == id, cancellationToken);
        }
    }
}
