using Delivery.Core.Exceptions;
using Delivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Delivery.Infrastructure.Data.Repositories
{
    public class TTNRepository(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<DeliveryDocument?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Documents
                .FirstOrDefaultAsync(dc => dc.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<IReadOnlyList<DeliveryDocument>> GetByObjectAsync(Guid objectid, CancellationToken cancellationToken = default)
        {
            return await _context.Documents
                .Where(dc => dc.Id == objectid)
                .OrderByDescending(dc => dc.ShippedAt)
                .AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Documents
                .AnyAsync(dc => dc.Id == id, cancellationToken: cancellationToken);
        }

        public async Task AddAsync(DeliveryDocument document, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.Documents.AddAsync(document, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException(
                    "Error adding new document",
                    nameof(DeliveryDocument),
                    document.Id,
                    ex);
            }
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            DeliveryDocument? document = await _context.Documents
                .FirstOrDefaultAsync(dc => dc.Id == id, cancellationToken: cancellationToken);

            if (document == null)
                return false;

            _context.Documents.Remove(document);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task DeleteAllForObjectAsync(Guid objectId, CancellationToken cancellationToken = default)
        {
            var documents = await GetByObjectAsync(objectId, cancellationToken);
            foreach (var document in documents)
                await DeleteAsync(document.Id, cancellationToken);
        }

    }
}
