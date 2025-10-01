using COM.API.Domain.Entities;
using COM.API.Domain.Enums;

namespace COM.API.Infrastructure.Interfaces
{
    public interface IObjectRepository
    {
        Task AddAsync(ConstructionObject constructionObject, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ConstructionObject?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateAsync(ConstructionObject constructionObject, CancellationToken cancellationToken = default);

        Task<List<ConstructionObject>> GetAllAsync(
            ObjectStatus? status = null,
            CancellationToken cancellationToken = default);
    }
}