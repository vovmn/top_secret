using WSS.API.Domain.Enums;

namespace WSS.API.DTOs.Requests
{
    /// <summary>
    /// Запрос на рассмотрение (согласование/отклонение) изменения графика.
    /// От службы строительного контроля.
    /// </summary>
    public class ReviewChangeRequestRequest
    {
        /// <summary>
        /// Новый статус запроса.
        /// </summary>
        public ChangeRequestStatus Status { get; set; }
    }
}
