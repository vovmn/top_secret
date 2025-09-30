using AutoMapper;
using WSS.API.Domain.Enums;
using WSS.API.DTOs.Requests;
using WSS.API.DTOs.Responses;
using WSS.API.Infrastructure.Interfaces;
using WSS.API.Integration.Interfaces;

namespace WSS.API.Services
{
    /// <summary>
    /// Сервис верификации выполненных работ службой контроля.
    /// Требует геопозиции и подтверждения нахождения на объекте.
    /// </summary>
    public class VerificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IObjectManagementClient _objectClient;
        private readonly IMapper _mapper;

        public VerificationService(
            IUnitOfWork unitOfWork,
            IObjectManagementClient objectClient,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _objectClient = objectClient;
            _mapper = mapper;
        }

        /// <summary>
        /// Подтверждает выполнение работы при посещении объекта.
        /// </summary>
        public async Task VerifyWorkAsync(Guid workItemId, VerifyWorkItemRequest verifyRequest, Guid verifiedBy)
        {
            var workItem = await _unitOfWork.WorkItems.GetByIdAsync(workItemId)
                ?? throw new KeyNotFoundException("Работа не найдена.");

            if (workItem.Status != WorkItemStatus.Completed)
                throw new InvalidOperationException("Работа не отмечена как выполненная.");

            var schedule = await _unitOfWork.WorkSchedules.GetByIdAsync(workItem.ScheduleId)
                ?? throw new InvalidOperationException("График не найден.");

            var isOnObject = await _objectClient.IsUserOnObjectAsync(
                schedule.ObjectId,
                verifyRequest.Latitude,
                verifyRequest.Longitude);

            if (!isOnObject)
                throw new InvalidOperationException("Верификация возможна только при нахождении на объекте.");

            workItem.Status = WorkItemStatus.Verified;
            workItem.VerifiedByControlId = verifiedBy;
            workItem.VerifiedAt = DateTime.UtcNow;

            await _unitOfWork.WorkItems.UpdateAsync(workItem);
            await _unitOfWork.CompletionReports.VerifyAsync(workItemId);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Получает все неподтверждённые работы по объекту.
        /// </summary>
        public async Task<UnverifiedWorkItemsResponse> GetUnverifiedWorksAsync(Guid objectId)
        {
            var unverified = await _unitOfWork.WorkItems.GetUnverifiedByObjectIdAsync(objectId);
            return new UnverifiedWorkItemsResponse
            {
                ObjectId = objectId,
                Items = _mapper.Map<List<WorkItemResponse>>(unverified)
            };
        }
    }
}
