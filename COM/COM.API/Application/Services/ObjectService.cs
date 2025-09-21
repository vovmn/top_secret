using COM.API.Application.Interfaces;
using COM.API.Domain.Entities;
using COM.API.Domain.Enums;
using COM.API.Domain.Events;
using COM.API.Domain.ValueObjects;
using COM.API.DTOs.Requests;
using COM.API.DTOs.Responses;
using COM.API.Infrastructure.Interfaces;

namespace COM.API.Application.Services
{
    /// <summary>
    /// Сервис для управления объектами благоустройства.
    /// Реализует бизнес-логику создания, активации и завершения объектов.
    /// </summary>
    public class ObjectService(
        IObjectRepository objectRepository,
        IChecklistService checklistService,
        IGeospatialService geospatialService,
        IDomainEventPublisher eventPublisher) : IObjectService
    {
        private readonly IObjectRepository _objectRepository = objectRepository;
        private readonly IChecklistService _checklistService = checklistService;
        private readonly IGeospatialService _geospatialService = geospatialService;
        private readonly IDomainEventPublisher _eventPublisher = eventPublisher;

        /// <summary>
        /// Создает новый объект благоустройства.
        /// Соответствует сценарию: "Для тех объектов, у которых наступает плановая дата начала работ, представитель службы строительного контроля активирует объект..."
        /// </summary>
        public async Task<ObjectResponse> CreateObjectAsync(CreateObjectRequest request, CancellationToken cancellationToken = default)
        {
            var polygon = new GeoPolygon(request.Polygon);
            var constructionObject = new ConstructionObject(
                Guid.NewGuid(),
                request.Name,
                request.Address,
                polygon,
                request.StartDate,
                request.EndDate);

            await _objectRepository.AddAsync(constructionObject, cancellationToken);

            return new ObjectResponse
            {
                Id = constructionObject.Id,
                Name = constructionObject.Name,
                Address = constructionObject.Address,
                Status = constructionObject.Status,
                StartDate = constructionObject.StartDate,
                EndDate = constructionObject.EndDate,
                Polygon = [.. constructionObject.Polygon.Coordinates]
            };
        }

        /// <summary>
        /// Активирует объект благоустройства.
        /// Соответствует сценарию: "активирует объект, становясь ответственным... назначает ответственного со стороны подрядчика... заполняет чек-лист... направляет инспектору КО".
        /// Публикует событие для уведомления других сервисов (например, Schedule Service).
        /// </summary>
        public async Task<ActivationResponse> ActivateObjectAsync(ActivateObjectRequest request, CancellationToken cancellationToken = default)
        {
            var constructionObject = await _objectRepository.GetByIdAsync(request.ObjectId, cancellationToken)
                ?? throw new KeyNotFoundException($"Объект с ID {request.ObjectId} не найден.");

            // Проверяем, что пользователь, активирующий объект, находится на его территории (ТЗ п.3)
            var userLocation = new Coordinate(request.UserLatitude, request.UserLongitude);
            if (!_geospatialService.IsPointInPolygon(userLocation, constructionObject.Polygon.Coordinates))
            {
                throw new InvalidOperationException("Активация объекта возможна только при физическом присутствии на его территории.");
            }

            // Загружаем чек-лист акта открытия
            var checklistResponse = await _checklistService.UploadChecklistAsync(
                new UploadChecklistRequest
                {
                    ConstructionObjectId = request.ObjectId,
                    File = request.ActFile,
                    FileName = request.ActFileName,
                    Type = ChecklistType.ActOpening,
                    Content = request.ChecklistAnswers // JSON-строка с ответами
                }, cancellationToken);

            var checklist = new Checklist(
                Guid.Parse(checklistResponse.Id),
                request.ObjectId,
                ChecklistType.ActOpening,
                checklistResponse.FileId,
                request.ChecklistAnswers);

            // Выполняем активацию через доменный метод
            constructionObject.Activate(
                request.ForemanUserId,
                request.InspectorSKUserId,
                request.InspectorKOUserId,
                checklist);

            // Сохраняем изменения
            await _objectRepository.UpdateAsync(constructionObject, cancellationToken);

            // Публикуем доменное событие
            await _eventPublisher.PublishAsync(new ObjectActivatedEvent(
                constructionObject.Id,
                DateTime.UtcNow,
                request.ForemanUserId,
                request.InspectorSKUserId,
                request.InspectorKOUserId), cancellationToken);

            return new ActivationResponse
            {
                Success = true,
                Message = "Объект успешно активирован.",
                ActivatedObjectId = constructionObject.Id,
                ChecklistId = checklist.Id
            };
        }

        /// <summary>
        /// Получает полную карточку объекта.
        /// </summary>
        public async Task<ObjectResponse> GetObjectByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var constructionObject = await _objectRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException($"Объект с ID {id} не найден.");

            return new ObjectResponse
            {
                Id = constructionObject.Id,
                Name = constructionObject.Name,
                Address = constructionObject.Address,
                Status = constructionObject.Status,
                StartDate = constructionObject.StartDate,
                EndDate = constructionObject.EndDate,
                Polygon = constructionObject.Polygon.Coordinates.ToList(),
                Responsibles = constructionObject.Responsibles.Select(r => new ResponsibleDto
                {
                    UserId = r.UserId,
                    Role = r.Role
                }).ToList(),
                Checklists = constructionObject.Checklists.Select(c => new ChecklistDto
                {
                    Id = c.Id.ToString(),
                    Type = c.Type,
                    CreatedAt = c.CreatedAt,
                    FileId = c.FileId
                }).ToList()
            };
        }

        /// <summary>
        /// Завершает объект, переводя его в статус "Completed".
        /// </summary>
        public async Task CompleteObjectAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var constructionObject = await _objectRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException($"Объект с ID {id} не найден.");

            constructionObject.Complete();
            await _objectRepository.UpdateAsync(constructionObject, cancellationToken);
        }
    }
}
