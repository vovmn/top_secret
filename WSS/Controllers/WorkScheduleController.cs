using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WSS.API.DTOs.Requests;
using WSS.API.DTOs.Responses;
using WSS.API.Services;

namespace WSS.API.Controllers
{
    [ApiController]
    [Route("schedules")]
    public class WorkScheduleController : ControllerBase
    {
        private readonly IWorkScheduleService _scheduleService;
        private readonly IChangeRequestService _changeRequestService;
        private readonly IVerificationService _verificationService;

        public WorkScheduleController(
            IWorkScheduleService scheduleService,
            IChangeRequestService changeRequestService,
            IVerificationService verificationService)
        {
            _scheduleService = scheduleService;
            _changeRequestService = changeRequestService;
            _verificationService = verificationService;
        }

        // === 1. Получение графика по объекту (все роли) ===
        [HttpGet("{objectId:guid}")]
        public async Task<ActionResult<WorkScheduleResponse>> GetSchedule(Guid objectId)
        {
            var schedule = await _scheduleService.GetScheduleByObjectIdAsync(objectId);
            return Ok(schedule);
        }

        // === 2. Прораб: отмечает работу как выполненную ===
        //[Authorize(Roles = "contractor_foreman")]
        [HttpPatch("{workItemId:guid}/complete")]
        public async Task<IActionResult> MarkWorkAsCompleted(
            Guid workItemId,
            [FromBody] SubmitWorkCompletionRequest request)
        {
            var userId = Guid.Parse(User.FindFirst("sub")?.Value ?? throw new UnauthorizedAccessException());

            await _scheduleService.MarkWorkAsCompletedAsync(
                workItemId,
                new UpdateWorkItemRequest
                {
                    Status = Domain.Enums.WorkItemStatus.Completed,
                    ActualStartDate = request.ActualStartDate,
                    ActualEndDate = request.ActualEndDate
                },
                new SubmitCompletionReportRequest
                {
                    PhotoFileId = request.PhotoFileId,
                    Comment = request.Comment,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude
                },
                userId
            );

            return NoContent();
        }

        // === 3. Прораб: предлагает изменение графика ===
        //[Authorize(Roles = "contractor_foreman")]
        [HttpPost("{objectId:guid}/change-requests")]
        public async Task<IActionResult> RequestScheduleChange(
            Guid objectId,
            [FromBody] CreateChangeRequestRequest request)
        {
            var userId = Guid.Parse(User.FindFirst("sub")?.Value ?? throw new UnauthorizedAccessException());
            await _changeRequestService.RequestChangeAsync(request, userId);
            return Ok();
        }

        // === 4. СК: получает нерассмотренные запросы на изменение ===
        //[Authorize(Roles = "construction_control")]
        [HttpGet("{objectId:guid}/change-requests/pending")]
        public async Task<ActionResult<List<ChangeRequestResponse>>> GetPendingChangeRequests(Guid objectId)
        {
            var requests = await _changeRequestService.GetPendingRequestsAsync(objectId);
            return Ok(requests);
        }

        // === 5. СК: одобряет/отклоняет запрос на изменение ===
        //[Authorize(Roles = "construction_control")]
        [HttpPatch("change-requests/{requestId:guid}/review")]
        public async Task<IActionResult> ReviewChangeRequest(
            Guid requestId,
            [FromBody] ReviewChangeRequestRequest review)
        {
            var userId = Guid.Parse(User.FindFirst("sub")?.Value ?? throw new UnauthorizedAccessException());
            await _changeRequestService.ReviewChangeRequestAsync(requestId, review, userId);
            return NoContent();
        }

        // === 6. СК: получает неподтверждённые работы ===
        //[Authorize(Roles = "construction_control")]
        [HttpGet("{objectId:guid}/unverified-works")]
        public async Task<ActionResult<UnverifiedWorkItemsResponse>> GetUnverifiedWorks(Guid objectId)
        {
            var response = await _verificationService.GetUnverifiedWorksAsync(objectId);
            return Ok(response);
        }

        // === 7. СК: подтверждает выполнение работы (требует геопозиции) ===
        //[Authorize(Roles = "construction_control")]
        [HttpPost("{workItemId:guid}/verify")]
        public async Task<IActionResult> VerifyWork(
            Guid workItemId,
            [FromBody] VerifyWorkItemRequest request)
        {
            var userId = Guid.Parse(User.FindFirst("sub")?.Value ?? throw new UnauthorizedAccessException());
            await _verificationService.VerifyWorkAsync(workItemId, request, userId);
            return NoContent();
        }

        // === 8. СК: редактирует график напрямую (без запроса) ===
        [Authorize(Roles = "construction_control")]
        [HttpPut("{objectId:guid}")]
        public async Task<IActionResult> UpdateScheduleDirectly(
            Guid objectId,
            [FromBody] UpdateWorkScheduleRequest request)
        {
            if (request.WorkItems == null || !request.WorkItems.Any())
                return BadRequest("Список работ для обновления не может быть пустым.");

            var userId = Guid.Parse(User.FindFirst("sub")?.Value
                ?? throw new UnauthorizedAccessException());

            await _scheduleService.UpdateScheduleDirectlyAsync(objectId, request.WorkItems, userId);

            return NoContent();
        }
    }
}
