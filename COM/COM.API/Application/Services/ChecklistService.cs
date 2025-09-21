using COM.API.Application.Interfaces;
using COM.API.Domain.Entities;
using COM.API.DTOs.Requests;
using COM.API.DTOs.Responses;
using COM.API.Infrastructure.Integration;
using COM.API.Infrastructure.Interfaces;

namespace COM.API.Application.Services
{
    /// <summary>
    /// Сервис для управления чек-листами и актами.
    /// Обрабатывает загрузку файлов в File Storage Service и сохранение метаданных в БД.
    /// </summary>
    public class ChecklistService(
        IChecklistRepository checklistRepository,
        FileStorageClient fileStorageClient) : IChecklistService
    {
        private readonly IChecklistRepository _checklistRepository = checklistRepository;
        private readonly FileStorageClient _fileStorageClient = fileStorageClient;

        /// <summary>
        /// Загружает файл чек-листа в File Storage Service и сохраняет запись в БД.
        /// </summary>
        public async Task<ChecklistResponse> UploadChecklistAsync(UploadChecklistRequest request, CancellationToken cancellationToken = default)
        {
            if (request.File == null)
                throw new ArgumentException("Файл чек-листа не предоставлен.", nameof(request.File));

            // Загружаем файл в File Storage Service
            string fileId = await _fileStorageClient.UploadFileAsync(request.File, request.FileName, cancellationToken);

            // Создаем сущность Checklist
            var checklist = new Checklist(
                Guid.NewGuid(),
                request.ConstructionObjectId,
                request.Type,
                fileId,
                request.Content);

            // Сохраняем в БД
            await _checklistRepository.AddAsync(checklist, cancellationToken);

            return new ChecklistResponse
            {
                Id = checklist.Id.ToString(),
                FileId = checklist.FileId,
                Type = checklist.Type,
                CreatedAt = checklist.CreatedAt
            };
        }
    }
}
