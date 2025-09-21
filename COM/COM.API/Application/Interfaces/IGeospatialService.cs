using COM.API.Domain.ValueObjects;

namespace COM.API.Application.Interfaces
{
    public interface IGeospatialService
    {
        bool IsPointInPolygon(Coordinate point, IReadOnlyList<Coordinate> polygonCoordinates);
    }
}