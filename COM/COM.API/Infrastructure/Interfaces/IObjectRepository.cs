using COM.API.Domain.Entities;

namespace COM.API.Infrastructure.Interfaces
{
    public interface IObjectRepository
    {
        Task AddAsync(ConstructionObject constructionObject, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ConstructionObject?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateAsync(ConstructionObject constructionObject, CancellationToken cancellationToken = default);
    }
}