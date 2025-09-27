using COM.API.Application.Interfaces;
using COM.API.Domain.Entities;
using COM.API.DTOs.Requests;
using COM.API.DTOs.Responses;
using COM.API.Infrastructure.Interfaces;

namespace COM.API.Application.Services
{
    /// <summary>
    /// Сервис для управления чек-листами и актами.
    /// Обрабатывает загрузку файлов в File Storage Service и сохранение метаданных в БД.
    /// </summary>
    public class ChecklistService(
        IUnitOfWork unitOfWork,
        IFileStorageClient fileStorageClientt) : IChecklistService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IFileStorageClient _fileStorageClient = fileStorageClientt;

        /// <summary>
        /// Загружает файл чек-листа в File Storage Service и сохраняет запись в БД.
        /// </summary>
        public async Task<ChecklistResponse> UploadChecklistAsync(UploadChecklistRequest request, CancellationToken cancellationToken = default)
        {
            if (request.File == null)
                throw new ArgumentException("Файл не предоставлен.", nameof(request.File));

            var fileId = await _fileStorageClient.UploadFileAsync(request.File, request.FileName, cancellationToken);

            var checklist = new Checklist(
                Guid.NewGuid(),
                request.ConstructionObjectId,
                request.Type,
                fileId,
                request.Content);

            await _unitOfWork.Checklists.AddAsync(checklist, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

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
