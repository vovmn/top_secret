namespace WSS.API.Domain.Enums
{
    /// <summary>
    /// Статус сетевого графика производства работ.
    /// </summary>
    public enum WorkScheduleStatus
    {
        /// <summary>
        /// Черновик (обычно не используется — график создаётся сразу активным).
        /// </summary>
        Draft,

        /// <summary>
        /// Активный график, используется в работе.
        /// </summary>
        Active,

        /// <summary>
        /// График заархивирован (объект завершён или отменён).
        /// </summary>
        Archived
    }
}
