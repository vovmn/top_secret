using COM.API.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace COM.API.Infrastructure.Data.Converters
{
    /// <summary>
    /// Конвертер значений для преобразования GeoPolygon <-> string (WKT) для хранения в PostgreSQL.
    /// Позволяет EF Core сохранять и загружать ваши доменные объекты-значения в/из базы данных.
    /// </summary>
    public class GeoPolygonConverter : ValueConverter<GeoPolygon, string>
    {
        public GeoPolygonConverter() : base(
        polygon => polygon.ToWkt(),           // GeoPolygon -> string (при сохранении в БД)
        wkt => GeoPolygon.FromWkt(wkt)        // string -> GeoPolygon (при загрузке из БД)
        )
        {
        }
    }
}
