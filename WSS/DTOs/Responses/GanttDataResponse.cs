using WSS.API.Domain.Enums;

namespace WSS.API.DTOs.Responses
{
    /// <summary>
    /// Упрощённый формат данных для отображения диаграммы Ганта в UI.
    /// Совместим с библиотеками типа dhtmlxGantt, Frappe Gantt.
    /// </summary>
    public class GanttDataResponse
    {
        public List<GanttTaskDto> Tasks { get; set; } = new();
    }

    public class GanttTaskDto
    {
        public string Id { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty; // "2024-06-01"
        public string EndDate { get; set; } = string.Empty;   // "2024-06-10"
        public WorkItemStatus Status { get; set; }

        /// <summary>
        /// Прогресс выполнения в диапазоне от 0.0 до 1.0.
        /// </summary>
        public decimal Progress => Status switch
        {
            WorkItemStatus.Verified => 1.0m,
            WorkItemStatus.Completed => 0.9m,
            _ => 0.0m
        };
    }
}
