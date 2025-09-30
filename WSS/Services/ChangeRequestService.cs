using AutoMapper;
using WSS.API.Domain.Entities;
using WSS.API.Domain.Enums;
using WSS.API.DTOs.Requests;
using WSS.API.DTOs.Responses;
using WSS.API.Infrastructure.Interfaces;
using WSS.API.Integration.Interfaces;

namespace WSS.API.Services
{
    /// <summary>
    /// Сервис управления запросами на изменение графика.
    /// </summary>
    public class ChangeRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationClient _notificationClient;
        private readonly IMapper _mapper;

        public ChangeRequestService(
            IUnitOfWork unitOfWork,
            INotificationClient notificationClient,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _notificationClient = notificationClient;
            _mapper = mapper;
        }

        /// <summary>
        /// Прораб создаёт запрос на изменение графика.
        /// </summary>
        public async Task RequestChangeAsync(CreateChangeRequestRequest request, Guid requestedBy)
        {
            var workItem = await _unitOfWork.WorkItems.GetByIdAsync(request.WorkItemId)
                ?? throw new KeyNotFoundException("Работа не найдена.");

            var schedule = await _unitOfWork.WorkSchedules.GetByIdAsync(workItem.ScheduleId);
            if (schedule?.Status != WorkScheduleStatus.Active)
                throw new InvalidOperationException("Изменение невозможно: график не активен.");

            var changeRequest = _mapper.Map<ScheduleChangeRequest>(request);
            changeRequest.Id = Guid.NewGuid();
            changeRequest.ScheduleId = schedule.Id;
            changeRequest.RequestedBy = requestedBy;
            changeRequest.RequestedAt = DateTime.UtcNow;

            await _unitOfWork.ChangeRequests.CreateAsync(changeRequest);
            await _unitOfWork.SaveChangesAsync();

            await _notificationClient.NotifyControlAboutChangeRequest(schedule.ObjectId);
        }

        /// <summary>
        /// Служба контроля рассматривает запрос.
        /// </summary>
        public async Task ReviewChangeRequestAsync(Guid requestId, ReviewChangeRequestRequest review, Guid reviewedBy)
        {
            var changeRequest = await _unitOfWork.ChangeRequests.GetByIdAsync(requestId);
            if (changeRequest == null || changeRequest.Status != ChangeRequestStatus.Pending)
                throw new InvalidOperationException("Запрос не найден или уже обработан.");

            await _unitOfWork.ChangeRequests.UpdateStatusAsync(requestId, review.Status, reviewedBy);

            if (review.Status == ChangeRequestStatus.Approved)
            {
                var workItem = await _unitOfWork.WorkItems.GetByIdAsync(changeRequest.WorkItemId);
                if (workItem != null)
                {
                    workItem.PlannedStartDate = changeRequest.NewStartDate;
                    workItem.PlannedEndDate = changeRequest.NewEndDate;
                    await _unitOfWork.WorkItems.UpdateAsync(workItem);
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Получает все нерассмотренные запросы по объекту.
        /// </summary>
        public async Task<List<ChangeRequestResponse>> GetPendingRequestsAsync(Guid objectId)
        {
            var requests = await _unitOfWork.ChangeRequests.GetPendingByObjectIdAsync(objectId);
            return _mapper.Map<List<ChangeRequestResponse>>(requests);
        }
    }
}
