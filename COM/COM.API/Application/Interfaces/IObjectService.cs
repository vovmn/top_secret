using COM.API.Domain.Entities;
using COM.API.Domain.Enums;
using COM.API.Domain.ValueObjects;
using COM.API.DTOs.Requests;
using COM.API.DTOs.Responses;

namespace COM.API.Application.Interfaces
{
    public interface IObjectService
    {
        Task<ActivationResponse> ActivateObjectAsync(ActivateObjectRequest request, CancellationToken cancellationToken = default);
        Task CompleteObjectAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ObjectResponse> CreateObjectAsync(CreateObjectRequest request, CancellationToken cancellationToken = default);
        Task<ObjectResponse> GetObjectByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<GeoPolygon> GetObjectPolygonAsync(Guid id, CancellationToken ct);

        Task<List<ConstructionObject>> GetObjectsForUserAsync(
        Guid userId,
        string userRole,
        ObjectStatus? status = null,
        CancellationToken cancellationToken = default);
    }
}