using AutoMapper;
using COM.API.Application.Interfaces;
using COM.API.Domain.Enums;
using COM.API.DTOs.Requests;
using COM.API.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace COM.API.Controllers
{
    /// <summary>
    /// Контроллер для управления объектами благоустройства.
    /// Реализует REST API для создания, активации, просмотра и завершения объектов.
    /// Соответствует пользовательским сценариям из ТЗ для роли "Служба строительного контроля".
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ObjectsController(IObjectService objectService, IMapper mapper) : BaseController
    {
        private readonly IObjectService _objectService = objectService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Создает новый объект благоустройства.
        /// [ТЗ: Служба СК создаёт объекты с привязкой к полигону и графику]
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ObjectResponse>> CreateObject([FromBody] CreateObjectRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _objectService.CreateObjectAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetObjectById), new { id = response.Id }, response);
        }

        /// <summary>
        /// Активирует объект благоустройства.
        /// [ТЗ: Служба СК назначает прораба, заполняет чек-лист, направляет инспектору КО]
        /// При активации проверяется геопозиция пользователя (должен находиться на объекте).
        /// </summary>
        [HttpPost("{id:guid}/activate")]
        public async Task<ActionResult<ActivationResponse>> ActivateObject(
            [FromRoute] Guid id,
            [FromForm] ActivateObjectFormRequest formRequest,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var request = new ActivateObjectRequest
            {
                ObjectId = id,
                ForemanUserId = formRequest.ForemanUserId,
                InspectorSKUserId = formRequest.InspectorSKUserId,
                InspectorKOUserId = formRequest.InspectorKOUserId,
                UserLatitude = formRequest.UserLatitude,
                UserLongitude = formRequest.UserLongitude,
                ActFile = formRequest.ActFile?.OpenReadStream(),
                ActFileName = formRequest.ActFile?.FileName,
                ChecklistAnswers = formRequest.ChecklistAnswers
            };

            var response = await _objectService.ActivateObjectAsync(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Получает полную информацию об объекте по ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ObjectResponse>> GetObjectById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var response = await _objectService.GetObjectByIdAsync(id, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Завершает объект, переводя его в статус "Completed".
        /// </summary>
        [HttpPost("{id:guid}/complete")]
        public async Task<IActionResult> CompleteObject([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await _objectService.CompleteObjectAsync(id, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Получает список объектов, доступных текущему пользователю.
        /// Прораб видит только свои объекты, СК — все, где он назначен или объекты в статусе Planned.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<ObjectResponse>>> GetObjects(
            [FromQuery] ObjectStatus? status = null,
            CancellationToken cancellationToken = default)
        {
            var userId = GetUserIdFromClaims(); // из JWT
            var userRole = GetUserRoleFromClaims(); // например: "construction_control", "foreman", "inspector_ko"

            var objects = await _objectService.GetObjectsForUserAsync(userId, userRole, status, cancellationToken);
            return Ok(_mapper.Map<List<ObjectResponse>>(objects));
        }

        /// <summary>
        /// Возвращает полигон объекта в формате GeoJSON.
        /// Используется для валидации геопозиции в других сервисах и отображения на карте.
        /// </summary>
        [HttpGet("{id:guid}/polygon")]
        public async Task<ActionResult<GeoJsonPolygonResponse>> GetObjectPolygon(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var polygon = await _objectService.GetObjectPolygonAsync(id, cancellationToken);

            return Ok(new GeoJsonPolygonResponse
            {
                Type = "Polygon",
                Coordinates = polygon.ToGeoJsonCoordinates()
            });
        }

        public class GeoJsonPolygonResponse
        {
            public string Type { get; set; } = "Polygon";
            public List<List<List<double>>> Coordinates { get; set; } = [];
        }
    }

    /// <summary>
    /// DTO для запроса активации объекта через multipart/form-data.
    /// </summary>
    public class ActivateObjectFormRequest
    {
        [FromForm]
        public Guid ForemanUserId { get; set; }

        [FromForm]
        public Guid InspectorSKUserId { get; set; }

        [FromForm]
        public Guid InspectorKOUserId { get; set; }

        [FromForm]
        public double UserLatitude { get; set; }

        [FromForm]
        public double UserLongitude { get; set; }

        [FromForm]
        public IFormFile? ActFile { get; set; }

        [FromForm]
        public string? ChecklistAnswers { get; set; }
    }
}
