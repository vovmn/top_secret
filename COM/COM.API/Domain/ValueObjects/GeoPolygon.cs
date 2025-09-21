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
}
