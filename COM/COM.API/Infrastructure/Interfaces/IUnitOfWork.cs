namespace COM.API.Infrastructure.Interfaces
{
    /// <summary>
    /// Интерфейс единицы работы (Unit of Work).
    /// Предоставляет доступ к репозиториям и управляет сохранением всех изменений одной транзакцией.
    /// Это гарантирует целостность данных, как того требует ТЗ (например, активация объекта + сохранение чек-листа).
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Получает репозиторий для сущности ConstructionObject.
        /// </summary>
        IChecklistRepository Checklists { get; }

        /// <summary>
        /// Получает репозиторий для сущности Checklist.
        /// </summary>
        IObjectRepository Objects { get; }

        /// <summary>
        /// Асинхронно сохраняет все изменения в базе данных одной транзакцией.
        /// </summary>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}