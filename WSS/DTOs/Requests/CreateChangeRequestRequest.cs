namespace WSS.API.DTOs.Requests
{
    /// <summary>
    /// Запрос на изменение графика работ.
    /// Инициируется прорабом.
    /// </summary>
    public class CreateChangeRequestRequest
    {
        /// <summary>
        /// Идентификатор работы, которую нужно изменить.
        /// </summary>
        public Guid WorkItemId { get; set; }

        /// <summary>
        /// Новые даты выполнения.
        /// </summary>
        public DateOnly NewStartDate { get; set; }
        public DateOnly NewEndDate { get; set; }

        /// <summary>
        /// Обоснование изменения.
        /// </summary>
        public string Reason { get; set; }
    }
}
