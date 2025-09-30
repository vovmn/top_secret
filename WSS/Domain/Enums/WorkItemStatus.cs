namespace WSS.API.Domain.Enums
{
    /// <summary>
    /// Статус выполнения отдельного вида работ.
    /// </summary>
    public enum WorkItemStatus
    {
        /// <summary>
        /// Работа запланирована, но ещё не начата.
        /// </summary>
        Planned,

        /// <summary>
        /// Работа начата (фактическая дата начала указана).
        /// </summary>
        InProgress,

        /// <summary>
        /// Прораб отметил работу как выполненную (ожидает верификации).
        /// </summary>
        Completed,

        /// <summary>
        /// Служба строительного контроля подтвердила выполнение.
        /// </summary>
        Verified
    }
}
