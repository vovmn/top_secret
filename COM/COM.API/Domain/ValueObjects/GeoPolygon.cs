namespace COM.API.Domain.ValueObjects
{
    public class GeoPolygon : ValueObject
    {
        public IReadOnlyList<Coordinate> Coordinates { get; private set; }

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

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var coord in Coordinates)
            {
                yield return coord.Latitude;
                yield return coord.Longitude;
            }
        }

        // Вспомогательный метод для проверки, находится ли точка внутри полигона
        // (Реализация алгоритма может быть вынесена в Infrastructure, но сигнатура — здесь)
        public bool Contains(Coordinate point)
        {
            // Здесь можно вызвать внешний сервис или использовать встроенную логику.
            // Для демонстрации — просто заглушка.
            throw new NotImplementedException("Реализация проверки точки в полигоне должна быть в Infrastructure.");
        }
    }

    // Вспомогательная структура для координат
    public record Coordinate(double Latitude, double Longitude);
}
