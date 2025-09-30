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
    /// Основной сервис управления сетевым графиком работ.
    /// Отвечает за создание, обновление и отображение графика по объекту.
    /// </summary>
    public class WorkScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IObjectManagementClient _objectClient;
        private readonly INotificationClient _notificationClient;
        private readonly IMapper _mapper;

        public WorkScheduleService(
            IUnitOfWork unitOfWork,
            IObjectManagementClient objectClient,
            INotificationClient notificationClient,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _objectClient = objectClient;
            _notificationClient = notificationClient;
            _mapper = mapper;
        }

        /// <summary>
        /// Инициализирует график при активации объекта (вызывается из Object Management).
        /// </summary>
        public async Task InitializeScheduleAsync(Guid objectId, List<CreateWorkItemRequest> workItems)
        {
            var objectExists = await _objectClient.ObjectExistsAndIsActiveAsync(objectId);
            if (!objectExists)
                throw new InvalidOperationException("Объект не найден или не активен.");

            var schedule = new WorkSchedule
            {
                Id = Guid.NewGuid(),
                ObjectId = objectId,
                Status = WorkScheduleStatus.Active,
                CreatedAt = DateTime.UtcNow,
                WorkItems = new List<WorkItem>()
            };

            foreach (var dto in workItems)
            {
                var item = _mapper.Map<WorkItem>(dto);
                item.Id = Guid.NewGuid();
                item.ScheduleId = schedule.Id;
                schedule.WorkItems.Add(item);
            }

            await _unitOfWork.WorkSchedules.CreateAsync(schedule);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Получает график по объекту для отображения в UI.
        /// </summary>
        public async Task<WorkScheduleResponse> GetScheduleByObjectIdAsync(Guid objectId)
        {
            var schedule = await _unitOfWork.WorkSchedules.GetByObjectIdAsync(objectId)
                ?? throw new KeyNotFoundException("График не найден.");
            return _mapper.Map<WorkScheduleResponse>(schedule);
        }

        /// <summary>
        /// Прораб отмечает работу как выполненную (с геопозицией и фото).
        /// </summary>
        public async Task MarkWorkAsCompletedAsync(
            Guid workItemId,
            UpdateWorkItemRequest updateRequest,
            SubmitCompletionReportRequest reportRequest,
            Guid reportedBy)
        {
            var workItem = await _unitOfWork.WorkItems.GetByIdAsync(workItemId)
                ?? throw new KeyNotFoundException("Работа не найдена.");

            if (workItem.Status == WorkItemStatus.Verified)
                throw new InvalidOperationException("Работа уже подтверждена.");

            // Обновляем статус и даты
            workItem.Status = updateRequest.Status;
            workItem.ActualStartDate ??= updateRequest.ActualStartDate;
            workItem.ActualEndDate = updateRequest.ActualEndDate;

            // Сохраняем отчёт
            var report = _mapper.Map<WorkCompletionReport>(reportRequest);
            report.WorkItemId = workItemId;
            report.ReportedBy = reportedBy;

            await _unitOfWork.CompletionReports.SaveAsync(report);
            await _unitOfWork.WorkItems.UpdateAsync(workItem);
            await _unitOfWork.SaveChangesAsync();

            // Уведомление
            var schedule = await _unitOfWork.WorkSchedules.GetByIdAsync(workItem.ScheduleId);
            if (schedule != null)
                await _notificationClient.NotifyControlAboutCompletedWork(schedule.ObjectId, workItemId);
        }
    }
}
