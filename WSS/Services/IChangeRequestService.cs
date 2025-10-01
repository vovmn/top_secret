using WSS.API.DTOs.Requests;
using WSS.API.DTOs.Responses;

namespace WSS.API.Services
{
    public interface IChangeRequestService
    {
        Task<List<ChangeRequestResponse>> GetPendingRequestsAsync(Guid objectId);
        Task RequestChangeAsync(CreateChangeRequestRequest request, Guid requestedBy);
        Task ReviewChangeRequestAsync(Guid requestId, ReviewChangeRequestRequest review, Guid reviewedBy);
    }
}