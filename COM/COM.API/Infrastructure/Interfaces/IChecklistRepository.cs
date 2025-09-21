using COM.API.Domain.Entities;

namespace COM.API.Infrastructure.Interfaces
{
    public interface IChecklistRepository
    {
        Task AddAsync(Checklist checklist, CancellationToken cancellationToken = default);
    }
}