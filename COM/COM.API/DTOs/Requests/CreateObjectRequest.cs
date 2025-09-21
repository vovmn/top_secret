using COM.API.Domain.ValueObjects;

namespace COM.API.DTOs.Requests
{
    /// <summary>
    /// DTO для создания нового объекта благоустройства.
    /// </summary>
    public class CreateObjectRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<Coordinate> Polygon { get; set; } = [];
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
