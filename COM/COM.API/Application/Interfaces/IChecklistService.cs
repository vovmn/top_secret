using COM.API.DTOs.Requests;
using COM.API.DTOs.Responses;

namespace COM.API.Application.Interfaces
{
    public interface IChecklistService
    {
        Task<ChecklistResponse> UploadChecklistAsync(UploadChecklistRequest request, CancellationToken cancellationToken = default);
    }
}