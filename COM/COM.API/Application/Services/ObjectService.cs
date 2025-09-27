using AutoMapper;
using COM.API.Application.Interfaces;
using COM.API.Domain.Entities;
using COM.API.Domain.Enums;
using COM.API.Domain.Events;
using COM.API.Domain.ValueObjects;
using COM.API.DTOs.Requests;
using COM.API.DTOs.Responses;
using COM.API.Infrastructure.Interfaces;
using COM.API.Profiles;

namespace COM.API.Application.Services
{
    /// <summary>
    /// Сервис для управления объектами благоустройства.
    /// Реализует бизнес-логику создания, активации и завершения объектов.
    /// </summary>
    public class ObjectService(
        IUnitOfWork unitOfWork,
        IFileStorageClient fileStorageClient,
        IGeospatialService geospatialService,
        IDomainEventPublisher eventPublisher,
        IMapper mapper) : IObjectService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork; // Единая точка входа для всех репозиториев
        private readonly IFileStorageClient _fileStorageClient = fileStorageClient;
        private readonly IGeospatialService _geospatialService = geospatialService;
        private readonly IDomainEventPublisher _eventPublisher = eventPublisher;
        private readonly IMapper _mapper = mapper;

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

            // 1. Регистрируем объект для добавления
            await _unitOfWork.Objects.AddAsync(constructionObject, cancellationToken);

            // 2. ЯВНО сохраняем ВСЕ изменения одной транзакцией
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ObjectResponse>(constructionObject);
        }

        /// <summary>
        /// Активирует объект благоустройства.
        /// Соответствует сценарию: "активирует объект, становясь ответственным... назначает ответственного со стороны подрядчика... заполняет чек-лист... направляет инспектору КО".
        /// Публикует событие для уведомления других сервисов (например, Schedule Service).
        /// </summary>
        public async Task<ActivationResponse> ActivateObjectAsync(ActivateObjectRequest request, CancellationToken cancellationToken = default)
        {
            var obj = await _unitOfWork.Objects.GetByIdAsync(request.ObjectId, cancellationToken)
               ?? throw new KeyNotFoundException($"Объект с ID {request.ObjectId} не найден.");

            // Early validation
            if (obj.Status != ObjectStatus.Planned)
                throw new InvalidOperationException("Активация возможна только для объектов в статусе 'Planned'.");

            // Геопроверка
            var userLocation = new Coordinate(request.UserLatitude, request.UserLongitude);
            if (!_geospatialService.IsPointInPolygon(userLocation, obj.Polygon.Coordinates))
                throw new InvalidOperationException("Активация объекта возможна только при физическом присутствии на его территории.");

            // Загрузка акта открытия
            string? fileId = null;
            if (request.ActFile != null)
            {
                fileId = await _fileStorageClient.UploadFileAsync(request.ActFile, request.ActFileName, cancellationToken);
            }

            // Создание чек-листа акта открытия
            var checklist = new Checklist(
                id: Guid.NewGuid(),
                constructionObjectId: request.ObjectId,
                type: ChecklistType.ActOpening,
                fileId: fileId,
                content: request.ChecklistAnswers);

            // Активация через домен
            obj.Activate(
                request.ForemanUserId,
                request.InspectorSKUserId,
                request.InspectorKOUserId,
                checklist);

            // Сохранение ВСЕГО в одной транзакции
            await _unitOfWork.Checklists.AddAsync(checklist, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Публикация события
            await _eventPublisher.PublishAsync(new ObjectActivatedEvent(
                obj.Id,
                DateTime.UtcNow,
                request.ForemanUserId,
                request.InspectorSKUserId,
                request.InspectorKOUserId), cancellationToken);

            return new ActivationResponse
            {
                Success = true,
                Message = "Объект успешно активирован.",
                ActivatedObjectId = obj.Id,
                ChecklistId = checklist.Id
            };
        }

        /// <summary>
        /// Получает полную карточку объекта.
        /// </summary>
        public async Task<ObjectResponse> GetObjectByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var obj = await _unitOfWork.Objects.GetByIdAsync(id, cancellationToken)
                 ?? throw new KeyNotFoundException($"Объект с ID {id} не найден.");

            return _mapper.Map<ObjectResponse>(obj);
        }

        /// <summary>
        /// Завершает объект, переводя его в статус "Completed".
        /// </summary>
        public async Task CompleteObjectAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var obj = await _unitOfWork.Objects.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException($"Объект с ID {id} не найден.");

            if (obj.Status != ObjectStatus.Active)
                throw new InvalidOperationException("Завершить можно только активный объект.");

            obj.Complete();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
