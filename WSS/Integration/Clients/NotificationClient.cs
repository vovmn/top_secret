using System.Text;
using System.Text.Json;
using WSS.API.Integration.Interfaces;

namespace WSS.API.Integration.Clients
{
    /// <summary>
    /// Реализация клиента для Notification Service.
    /// </summary>
    public class NotificationClient : INotificationClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NotificationClient> _logger;

        public NotificationClient(HttpClient _httpClient, ILogger<NotificationClient> logger)
        {
            _httpClient = _httpClient;
            _logger = logger;
        }

        public async Task NotifyControlAboutCompletedWork(Guid objectId, Guid workItemId)
        {
            try
            {
                var payload = new
                {
                    Type = "WorkCompleted",
                    ObjectId = objectId,
                    WorkItemId = workItemId,
                    Timestamp = DateTime.UtcNow
                };

                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                await _httpClient.PostAsync("/api/notifications/send", content);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Не удалось отправить уведомление о завершении работ");
                // Не бросаем исключение — уведомление не критично для транзакции
            }
        }

        public async Task NotifyControlAboutChangeRequest(Guid objectId)
        {
            try
            {
                var payload = new
                {
                    Type = "ScheduleChangeRequested",
                    ObjectId = objectId,
                    Timestamp = DateTime.UtcNow
                };

                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                await _httpClient.PostAsync("/api/notifications/send", content);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Не удалось отправить уведомление о запросе на изменение графика");
            }
        }
    }
}
