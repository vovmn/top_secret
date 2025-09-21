using COM.API.Domain.Enums;
using COM.API.Domain.ValueObjects;

namespace COM.API.Domain.Entities
{
    public class ConstructionObject
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public ObjectStatus Status { get; private set; } // Статус объекта
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public GeoPolygon Polygon { get; private set; } // Границы объекта

        // Ответственные лица (связь один-ко-многим)
        private readonly List<ObjectResponsible> _responsibles = new();
        public IReadOnlyCollection<ObjectResponsible> Responsibles => _responsibles.AsReadOnly();

        // Чек-листы и акты (связь один-ко-многим)
        private readonly List<Checklist> _checklists = new();
        public IReadOnlyCollection<Checklist> Checklists => _checklists.AsReadOnly();

        // Конструктор для создания нового объекта
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

        // Метод для активации объекта (бизнес-правило!)
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

        // Метод для завершения объекта
        public void Complete()
        {
            if (Status != ObjectStatus.Active)
                throw new InvalidOperationException("Завершить можно только активный объект.");

            Status = ObjectStatus.Completed;
        }

        // Приватный конструктор для EF Core
        private ConstructionObject() { }
    }
}
