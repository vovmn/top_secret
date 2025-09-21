using COM.API.Domain.Enums;

namespace COM.API.Domain.Entities
{
    /// <summary>
    /// Сущность, представляющая чек-лист или акт, связанный с объектом благоустройства.
    /// Соответствует требованиям ТЗ по прикреплению акта открытия объекта (PDF/изображение) и заполнению чек-листа.
    /// </summary>
    public class Checklist
    {
        /// <summary>
        /// Уникальный идентификатор чек-листа.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// ID объекта благоустройства, к которому относится чек-лист.
        /// </summary>
        public Guid ConstructionObjectId { get; private set; }

        /// <summary>
        /// Тип чек-листа (например, акт открытия, акт закрытия).
        /// </summary>
        public ChecklistType Type { get; private set; }

        /// <summary>
        /// Дата и время создания чек-листа.
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Уникальный идентификатор файла в системе File Storage Service (например, "uploads/acts/abc123.pdf").
        /// Используется для хранения ссылок на PDF или изображения, как указано в ТЗ.
        /// </summary>
        public string? FileId { get; private set; }

        /// <summary>
        /// JSON-строка, содержащая структурированные ответы на вопросы чек-листа (опционально).
        /// Например: { "question1": "Да", "question2": "Нет" }.
        /// </summary>
        public string? Content { get; private set; }

        /// <summary>
        /// Конструктор для создания нового чек-листа.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="constructionObjectId">ID объекта благоустройства.</param>
        /// <param name="type">Тип чек-листа.</param>
        /// <param name="fileId">ID файла в хранилище (опционально).</param>
        /// <param name="content">Структурированное содержимое в формате JSON (опционально).</param>
        public Checklist(
        Guid id,
        Guid constructionObjectId,
        ChecklistType type,
        string? fileId = null,
        string? content = null)
        {
            Id = id;
            ConstructionObjectId = constructionObjectId;
            Type = type;
            CreatedAt = DateTime.UtcNow;
            FileId = fileId;
            Content = content;
        }

        /// <summary>
        /// Приватный конструктор для EF Core
        /// </summary>
        private Checklist() { }
    }
}
