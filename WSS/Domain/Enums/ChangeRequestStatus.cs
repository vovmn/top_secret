namespace WSS.API.Domain.Enums
{
    /// <summary>
    /// Статус запроса на изменение графика.
    /// </summary>
    public enum ChangeRequestStatus
    {
        /// <summary>
        /// Запрос отправлен, ожидает рассмотрения.
        /// </summary>
        Pending,

        /// <summary>
        /// Запрос согласован службой контроля.
        /// </summary>
        Approved,

        /// <summary>
        /// Запрос отклонён.
        /// </summary>
        Rejected
    }
}
