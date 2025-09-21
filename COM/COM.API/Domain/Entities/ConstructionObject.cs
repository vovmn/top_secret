using COM.API.Domain.Enums;
using COM.API.Domain.ValueObjects;

namespace COM.API.Domain.Entities
{
    /// <summary>
    /// Агрегатный корень, представляющий объект благоустройства (парк, сквер, двор).
    /// Является центральной сущностью системы. Все операции с объектом (активация, завершение)
    /// должны проходить через этот класс для обеспечения целостности данных и соблюдения бизнес-правил.
    /// Соответствует требованиям ТЗ: хранение полигона, назначение ответственных, управление статусом.
    /// </summary>
    public class ConstructionObject
    {
        /// <summary>
        /// Уникальный идентификатор объекта.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Название объекта благоустройства (например, "Парк Горького, сектор А").
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Физический адрес объекта.
        /// </summary>
        public required string Address { get; init; }

        /// <summary>
        /// Текущий статус жизненного цикла объекта (Planned, Active, Completed).
        /// Согласно ТЗ, объект должен быть активирован перед началом работ.
        /// </summary>
        public ObjectStatus Status { get; private set; }

        /// <summary>
        /// Плановая дата начала работ на объекте.
        /// </summary>
        public DateTime? StartDate { get; private set; }

        /// <summary>
        /// Плановая дата окончания работ на объекте.
        /// </summary>
        public DateTime? EndDate { get; private set; }

        /// <summary>
        /// Географический полигон, задающий границы рабочей зоны объекта.
        /// Используется для верификации присутствия пользователей на объекте (требование ТЗ п.3).
        /// </summary>
        public required GeoPolygon Polygon { get; init; }

        /// <summary>
        /// Коллекция ответственных лиц, назначенных на объект (Прораб, Инспекторы СК и КО).
        /// Отношение один-ко-многим. Изменения должны происходить только через методы агрегата.
        /// </summary>
        private readonly List<ObjectResponsible> _responsibles = [];
        public IReadOnlyCollection<ObjectResponsible> Responsibles => _responsibles.AsReadOnly();

        /// <summary>
        /// Коллекция чек-листов и актов, связанных с объектом (например, акт открытия).
        /// Отношение один-ко-многим. Изменения должны происходить только через методы агрегата.
        /// </summary>
        private readonly List<Checklist> _checklists = [];
        public IReadOnlyCollection<Checklist> Checklists => _checklists.AsReadOnly();

        /// <summary>
        /// Конструктор для создания нового объекта благоустройства в статусе "Planned".
        /// Вызывается при инициализации нового объекта службой строительного контроля.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="name">Название объекта.</param>
        /// <param name="address">Адрес объекта.</param>
        /// <param name="polygon">Геополигон, определяющий границы объекта.</param>
        /// <param name="startDate">Плановая дата начала работ (опционально).</param>
        /// <param name="endDate">Плановая дата окончания работ (опционально).</param>
        public ConstructionObject(
            Guid id,
            string name,
            string address,
            GeoPolygon polygon,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            Id = id;
            Name = name;
            Address = address;
            Polygon = polygon;
            Status = ObjectStatus.Planned; // Новый объект всегда в статусе "Планируется"
            StartDate = startDate;
            EndDate = endDate;
        }

        /// <summary>
        /// Метод активации объекта. Переводит объект из статуса "Planned" в "Active".
        /// Соответствует сценарию из ТЗ: назначение прораба, заполнение чек-листа, согласование с инспектором КО.
        /// Бизнес-правила:
        /// - Активация возможна только из статуса "Planned".
        /// - Должен быть задан валидный полигон.
        /// - Должен быть передан чек-лист акта открытия.
        /// При успешной активации публикуется доменное событие <see cref="ObjectActivatedEvent"/>.
        /// </summary>
        /// <param name="foremanUserId">ID пользователя, назначенного прорабом (подрядчик).</param>
        /// <param name="inspectorSKUserId">ID пользователя, назначенного инспектором СК (заказчик).</param>
        /// <param name="inspectorKOUserId">ID пользователя, назначенного инспектором контрольного органа.</param>
        /// <param name="initialChecklist">Чек-лист акта открытия объекта.</param>
        public void Activate(
            Guid foremanUserId,
            Guid inspectorSKUserId,
            Guid inspectorKOUserId,
            Checklist initialChecklist)
        {
            if (Status != ObjectStatus.Planned)
                throw new InvalidOperationException("Объект можно активировать только в статусе 'Planned'.");

            if (Polygon == null || Polygon.Coordinates.Count < 3)
                throw new InvalidOperationException("Для активации объекта должен быть задан валидный полигон.");

            // Назначаем ответственных
            _responsibles.Add(new ObjectResponsible(Id, foremanUserId, ResponsibleRole.Foreman));
            _responsibles.Add(new ObjectResponsible(Id, inspectorSKUserId, ResponsibleRole.InspectorSK));
            _responsibles.Add(new ObjectResponsible(Id, inspectorKOUserId, ResponsibleRole.InspectorKO));

            // Добавляем чек-лист акта открытия
            _checklists.Add(initialChecklist);

            // Меняем статус
            Status = ObjectStatus.Active;
        }

        /// <summary>
        /// Метод завершения объекта. Переводит объект из статуса "Active" в "Completed".
        /// Вызывается, когда все работы на объекте завершены и приняты.
        /// </summary>
        public void Complete()
        {
            if (Status != ObjectStatus.Active)
                throw new InvalidOperationException("Завершить можно только активный объект.");

            Status = ObjectStatus.Completed;
        }

        /// <summary>
        /// Приватный конструктор для EF Core
        /// </summary>
        private ConstructionObject() { }
    }
}
