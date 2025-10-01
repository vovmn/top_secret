using WSS.API.DTOs.Requests;
using WSS.API.DTOs.Responses;

namespace WSS.API.Services
{
    public interface IVerificationService
    {
        Task<UnverifiedWorkItemsResponse> GetUnverifiedWorksAsync(Guid objectId);
        Task VerifyWorkAsync(Guid workItemId, VerifyWorkItemRequest verifyRequest, Guid verifiedBy);
    }
}