using WSS.API.Integration.Interfaces;

namespace WSS.API.Integration.Clients
{
    /// <summary>
    /// Реализация клиента для Object Management Service через HttpClient.
    /// </summary>
    public class ObjectManagementClient : IObjectManagementClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ObjectManagementClient> _logger;

        public ObjectManagementClient(HttpClient httpClient, ILogger<ObjectManagementClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> ObjectExistsAndIsActiveAsync(Guid objectId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/objects/{objectId}/status");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при проверке объекта {ObjectId}", objectId);
                return false; // или throw, в зависимости от политики
            }
        }

        public async Task<bool> IsUserOnObjectAsync(Guid objectId, double latitude, double longitude)
        {
            try
            {
                var query = $"?lat={latitude}&lon={longitude}";
                var response = await _httpClient.GetAsync($"/api/objects/{objectId}/is-on-object{query}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return bool.Parse(content); // или десериализовать JSON
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при проверке геопозиции для объекта {ObjectId}", objectId);
                return false;
            }
        }
    }
}
