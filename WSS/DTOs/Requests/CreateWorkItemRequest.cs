namespace WSS.API.DTOs.Requests
{
    /// <summary>
    /// Запрос на создание нового вида работ в графике.
    /// Используется при инициализации графика или добавлении работ.
    /// </summary>
    public class CreateWorkItemRequest
    {
        /// <summary>
        /// Наименование вида работ.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Плановая дата начала.
        /// </summary>
        public DateOnly PlannedStartDate { get; set; }

        /// <summary>
        /// Плановая дата окончания.
        /// </summary>
        public DateOnly PlannedEndDate { get; set; }

        /// <summary>
        /// Идентификатор прораба (опционально).
        /// </summary>
        public Guid? ResponsibleContractorId { get; set; }
    }
}
