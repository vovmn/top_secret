using COM.API.Application.Interfaces;
using COM.API.Domain.Enums;
using COM.API.DTOs.Requests;
using COM.API.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace COM.API.Controllers
{
    /// <summary>
    /// Контроллер для управления чек-листами и актами.
    /// Позволяет загружать файлы актов (например, акт открытия) и сохранять их метаданные.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ChecklistsController(IChecklistService checklistService) : ControllerBase
    {
        private readonly IChecklistService _checklistService = checklistService;

        /// <summary>
        /// Загружает чек-лист (акт) для объекта.
        /// [ТЗ: Прикрепление акта открытия объекта в формате pdf или изображения]
        /// </summary>

        [HttpPost("Upload")]
        public async Task<ActionResult<ChecklistResponse>> UploadChecklist([FromForm] UploadChecklistFormRequest formRequest, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (formRequest.File == null)
                return BadRequest("Файл акта обязателен.");

            var request = new UploadChecklistRequest
            {
                ConstructionObjectId = formRequest.ConstructionObjectId,
                File = formRequest.File.OpenReadStream(),
                FileName = formRequest.File.FileName,
                Type = formRequest.Type,
                Content = formRequest.Content
            };

            var response = await _checklistService.UploadChecklistAsync(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Получает список всех чек-листов и актов по объекту.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<ChecklistResponse>>> GetChecklistsByObject(
            [FromQuery] Guid constructionObjectId,
            CancellationToken cancellationToken = default)
        {
            var checklists = await _checklistService.GetChecklistsByObjectAsync(constructionObjectId, cancellationToken);
            return Ok(checklists);
        }
    }
    public class UploadChecklistFormRequest
    {
        [FromForm]
        public Guid ConstructionObjectId { get; set; }

        [FromForm]
        public IFormFile File { get; set; } = null!;

        [FromForm]
        public ChecklistType Type { get; set; }

        [FromForm]
        public string? Content { get; set; }
    }
}
