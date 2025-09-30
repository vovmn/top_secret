using WSS.API.Domain.Enums;

namespace WSS.API.Domain.Entities
{
    /// <summary>
    /// Сетевой график производства работ по объекту благоустройства.
    /// Создаётся при активации объекта и привязан строго к одному ObjectId.
    /// </summary>
    public class WorkSchedule
    {
        /// <summary>
        /// Уникальный идентификатор графика.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор объекта благоустройства (из Object Management Service).
        /// </summary>
        public Guid ObjectId { get; set; }

        /// <summary>
        /// Текущий статус графика.
        /// </summary>
        public WorkScheduleStatus Status { get; set; } = WorkScheduleStatus.Active;

        /// <summary>
        /// Номер версии для отслеживания изменений (оптимистичная блокировка).
        /// </summary>
        public int Version { get; set; } = 1;

        /// <summary>
        /// Дата и время создания графика.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Коллекция работ, входящих в график.
        /// </summary>
        public List<WorkItem> WorkItems { get; set; } = new();
    }
}
