using COM.API.Application.Interfaces;
using NetTopologySuite.Geometries;

namespace COM.API.Application.Services
{
    /// <summary>
    /// Сервис для выполнения геопространственных операций.
    /// Реализует проверку, находится ли точка внутри полигона, с использованием библиотеки NetTopologySuite.
    /// Вынесен в инфраструктурный слой, чтобы сохранить чистоту доменного слоя.
    /// </summary>
    public class GeospatialService : IGeospatialService
    {
        /// <summary>
        /// Проверяет, находится ли заданная точка внутри полигона.
        /// </summary>
        /// <param name="point">Проверяемая точка (широта, долгота).</param>
        /// <param name="polygonCoordinates">Список координат, задающих полигон.</param>
        /// <returns>True, если точка внутри полигона; иначе false.</returns>
        public bool IsPointInPolygon(Domain.ValueObjects.Coordinate point, IReadOnlyList<Domain.ValueObjects.Coordinate> polygonCoordinates)
        {
            if (polygonCoordinates.Count < 3)
                return false;

            // Создаем геометрию полигона с помощью NetTopologySuite
            var shell = new LinearRing([.. polygonCoordinates.Select(c => new Coordinate(c.Longitude, c.Latitude))]);

            var polygon = new Polygon(shell);
            var ntsPoint = new Point(new Coordinate(point.Longitude, point.Latitude));

            // Проверяем, содержит ли полигон точку
            return polygon.Contains(ntsPoint);
        }
    }
}
