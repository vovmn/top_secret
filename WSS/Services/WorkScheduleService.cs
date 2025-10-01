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
    public class WorkScheduleService : IWorkScheduleService
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

            var schedule = await _unitOfWork.WorkSchedules.GetByIdAsync(workItem.ScheduleId);
            if (schedule == null) throw new InvalidOperationException("График не найден.");

            if (schedule?.Status != WorkScheduleStatus.Active)
                throw new InvalidOperationException("График не активен.");

            if (workItem.Status == WorkItemStatus.Verified)
                throw new InvalidOperationException("Работа уже подтверждена.");

            var isOnObject = await _objectClient.IsUserOnObjectAsync(
                schedule.ObjectId,
                reportRequest.Latitude,
                reportRequest.Longitude);
            if (!isOnObject)
                throw new InvalidOperationException("Завершение работ возможно только при нахождении на объекте.");

            // Проверка даты
            var today = DateTime.UtcNow.Date;
            if (updateRequest.ActualEndDate?.Date != today)
                throw new InvalidOperationException("Фактическая дата завершения должна быть сегодняшней.");

            // Обновление
            workItem.Status = WorkItemStatus.Completed;
            workItem.ActualStartDate ??= updateRequest.ActualStartDate;
            workItem.ActualEndDate = updateRequest.ActualEndDate;

            var report = _mapper.Map<WorkCompletionReport>(reportRequest);
            report.WorkItemId = workItemId;
            report.ReportedBy = reportedBy;

            await _unitOfWork.CompletionReports.SaveAsync(report);
            await _unitOfWork.WorkItems.UpdateAsync(workItem);
            await _unitOfWork.SaveChangesAsync();

            await _notificationClient.NotifyControlAboutCompletedWork(schedule.ObjectId, workItemId);
        }

        public async Task UpdateScheduleDirectlyAsync(Guid objectId, List<UpdateWorkItemRequest> updates, Guid updatedBy)
        {
            // 1. Проверяем существование и статус графика
            var schedule = await _unitOfWork.WorkSchedules.GetByObjectIdAsync(objectId)
                ?? throw new KeyNotFoundException("График работ не найден для указанного объекта.");

            if (schedule.Status != WorkScheduleStatus.Active)
                throw new InvalidOperationException("Изменение графика возможно только в статусе 'Активен'.");

            // 2. Обновляем каждую работу
            foreach (var update in updates)
            {
                var workItem = await _unitOfWork.WorkItems.GetByIdAsync(update.Id);
                if (workItem == null || workItem.ScheduleId != schedule.Id)
                    throw new ArgumentException($"Работа с ID {update.Id} не принадлежит графику объекта {objectId}.");

                // Обновляем ТОЛЬКО плановые даты (СК не меняет фактические!)
                if (update.PlannedStartDate.HasValue)
                    workItem.PlannedStartDate = update.PlannedStartDate.Value.Date; // ← обрезаем время
                if (update.PlannedEndDate.HasValue)
                    workItem.PlannedEndDate = update.PlannedEndDate.Value.Date;

                // Опционально: можно обновить и другие поля (например, Name), если нужно

                await _unitOfWork.WorkItems.UpdateAsync(workItem);
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
