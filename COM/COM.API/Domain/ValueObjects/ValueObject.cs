namespace COM.API.Domain.ValueObjects
{
    /// <summary>
    /// Базовый класс для всех объектов-значений (Value Objects) в системе.
    /// Обеспечивает корректное сравнение объектов по их свойствам (а не по ссылке)/
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        /// Определяет компоненты (свойства), которые участвуют в сравнении двух объектов-значений.
        /// Этот метод должен быть переопределен в каждом производном классе для указания,
        /// какие свойства определяют "тождественность" объекта.
        /// </summary>
        /// <returns>Перечисление компонентов для сравнения.</returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// Переопределенный оператор равенства.
        /// Сравнивает два объекта-значения по их компонентам.
        /// </summary>
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        /// <summary>
        /// Переопределенный оператор неравенства.
        /// </summary>
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Переопределенный метод Equals.
        /// Сравнивает текущий объект с другим объектом по компонентам, возвращаемым GetEqualityComponents().
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Переопределенный метод GetHashCode.
        /// Генерирует хэш-код на основе компонентов объекта.
        /// Необходим для корректной работы в коллекциях типа HashSet, Dictionary и т.д.
        /// </summary>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y); // XOR всех хэшей компонентов
        }
    }
}
