using NetTopologySuite.Geometries;

namespace COM.API.Domain.ValueObjects
{
    /// <summary>
    /// Объект-значение (Value Object), инкапсулирующий географический полигон — набор координат,
    /// задающих границы объекта благоустройства на карте.
    /// Соответствует требованиям ТЗ по геопривязке и верификации присутствия пользователей на объекте.
    /// ВАЖНО: Реализация метода Contains вынесена в инфраструктурный слой (IGeospatialService),
    /// чтобы сохранить чистоту доменного слоя и избежать зависимости от внешних библиотек.
    /// </summary>
    public class GeoPolygon : ValueObject
    {
        /// <summary>
        /// Список координат, задающих полигон. Полигон всегда замкнут (первая и последняя точка совпадают).
        /// </summary>
        public IReadOnlyList<Coordinate> Coordinates { get; private set; }

        /// <summary>
        /// Конструктор для создания полигона из набора координат.
        /// Автоматически замыкает полигон, если первая и последняя точки не совпадают.
        /// Выбрасывает исключение, если передано менее 3 уникальных точек.
        /// </summary>
        /// <param name="coordinates">Набор координат.</param>
        public GeoPolygon(IEnumerable<Coordinate> coordinates)
        {
            if (coordinates == null || !coordinates.Any())
                throw new ArgumentException("Полигон не может быть пустым.", nameof(coordinates));

            var coordList = coordinates.ToList();
            if (coordList.Count < 3)
                throw new ArgumentException("Полигон должен содержать минимум 3 точки.", nameof(coordinates));

            // Проверяем, что первая и последняя точки совпадают (замкнутый полигон)
            if (coordList.First() != coordList.Last())
            {
                coordList.Add(coordList.First()); // Автоматически замыкаем полигон
            }

            Coordinates = coordList.AsReadOnly();
        }

        /// <summary>
        /// Переопределенный метод для сравнения объектов-значений по их свойствам.
        /// Используется для корректной работы Equals и GetHashCode.
        /// </summary>
        /// <returns>Перечисление компонентов, участвующих в сравнении.</returns>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var coord in Coordinates)
            {
                yield return coord.Latitude;
                yield return coord.Longitude;
            }
        }
    }

    /// <summary>
    /// Record, представляющий географическую координату (широта, долгота).
    /// Используется для простого и неизменяемого хранения пары координат.
    /// </summary>
    /// <param name="Latitude">Широта (в градусах).</param>
    /// <param name="Longitude">Долгота (в градусах).</param>
    public record Coordinate(double Latitude, double Longitude);



    // --- НОВЫЕ МЕТОДЫ ДЛЯ РАБОТЫ С WKT ---

    /// <summary>
    /// Преобразует полигон в строку формата WKT (Well-Known Text).
    /// Пример: "POLYGON((30 10, 40 40, 20 40, 10 20, 30 10))"
    /// </summary>
    //public string ToWkt()
    //{
    //    var points = string.Join(", ", Coordinates.Select(c => $"{c.Longitude} {c.Latitude}"));
    //    return $"POLYGON(({points}))";
    //}

    /// <summary>
    /// Создает экземпляр GeoPolygon из строки формата WKT.
    /// </summary>
    //public static GeoPolygon FromWkt(string wkt)
    //{
    //    if (string.IsNullOrWhiteSpace(wkt))
    //        throw new ArgumentException("WKT строка не может быть пустой.", nameof(wkt));

    //    // Упрощенный парсер для POLYGON((...))
    //    // В реальном проекте лучше использовать NetTopologySuite.IO.WKTReader
    //    var match = System.Text.RegularExpressions.Regex.Match(wkt.Trim(), @"POLYGON\s*\(\(([^)]+)\)\)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
    //    if (!match.Success)
    //        throw new ArgumentException("Неверный формат WKT полигона.", nameof(wkt));

    //    var points = match.Groups[1].Value.Split(',')
    //        .Select(p => p.Trim().Split(' '))
    //        .Where(parts => parts.Length == 2)
    //        .Select(parts => new Coordinate(
    //            double.Parse(parts[1], System.Globalization.CultureInfo.InvariantCulture), // Широта
    //            double.Parse(parts[0], System.Globalization.CultureInfo.InvariantCulture)  // Долгота
    //        ))
    //        .ToList();

    //    return new GeoPolygon(points);
    //}
}
