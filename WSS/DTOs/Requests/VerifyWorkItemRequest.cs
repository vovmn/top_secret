namespace WSS.API.DTOs.Requests
{
    /// <summary>
    /// Запрос на верификацию выполненной работы.
    /// От службы строительного контроля при посещении объекта.
    /// </summary>
    public class VerifyWorkItemRequest
    {
        /// <summary>
        /// Широта местоположения при верификации.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Долгота местоположения при верификации.
        /// </summary>
        public double Longitude { get; set; }
    }
}
