using COM.API.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace COM.API.Application.Services
{
    /// <summary>
    /// Реализация публикатора доменных событий с использованием RabbitMQ.
    /// Позволяет другим микросервисам подписываться на события и реагировать на них асинхронно.
    /// Это обеспечивает слабую связанность и масштабируемость системы.
    /// </summary>
    public class DomainEventPublisher : IAsyncDisposable, IDomainEventPublisher
    {
        private readonly IConnection _connection;
        private readonly IChannel? _channel;
        private bool _disposed = false;

        public DomainEventPublisher(IConfiguration configuration)
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMQ:HostName"] ?? "localhost",
                Port = int.Parse(configuration["RabbitMQ:Port"] ?? "5672"),
                UserName = configuration["RabbitMQ:UserName"] ?? "guest",
                Password = configuration["RabbitMQ:Password"] ?? "guest"
            };

            // Используем асинхронный метод для создания подключения
            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();

            // Используем асинхронный метод для создания канала
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();

            // Объявляем обменник (exchange) типа "topic" для гибкой маршрутизации событий
            _channel.ExchangeDeclareAsync(exchange: "domain_events", type: ExchangeType.Topic, durable: true)
                    .GetAwaiter().GetResult();
        }

        /// <summary>
        /// Публикует доменное событие в RabbitMQ.
        /// Событие сериализуется в JSON и отправляется в обменник с ключом маршрутизации, 
        /// построенным по имени типа события (например, "construction.object.activated").
        /// </summary>
        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : class
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));

            var eventName = GetEventName<TEvent>();
            var eventBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));

            // Публикация сообщения (асинхронно)
            await _channel.BasicPublishAsync(
                exchange: "domain_events",
                routingKey: eventName,
                body: eventBody,
                mandatory: false,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Генерирует ключ маршрутизации на основе типа события.
        /// Например, для ObjectActivatedEvent -> "construction.object.activated".
        /// Это позволяет подписчикам фильтровать события по интересующим их типам.
        /// </summary>
        private string GetEventName<TEvent>() where TEvent : class
        {
            var typeName = typeof(TEvent).Name;
            // Пример: "ObjectActivatedEvent" -> "construction.object.activated"
            if (typeName.EndsWith("Event"))
            {
                typeName = typeName[..^5]; // Убираем "Event"
            }

            // Разделяем PascalCase на слова (ObjectActivated -> object.activated)
            var words = SplitPascalCase(typeName);
            return $"construction.{string.Join(".", words).ToLower()}";
        }

        /// <summary>
        /// Вспомогательный метод для разделения строки в стиле PascalCase на отдельные слова.
        /// Например, "ObjectActivated" -> ["Object", "Activated"].
        /// </summary>
        private IEnumerable<string> SplitPascalCase(string input)
        {
            var words = new List<string>();
            var currentWord = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]) && i > 0 && !char.IsUpper(input[i - 1]))
                {
                    words.Add(currentWord.ToString());
                    currentWord.Clear();
                }
                currentWord.Append(input[i]);
            }

            if (currentWord.Length > 0)
            {
                words.Add(currentWord.ToString());
            }

            return words;
        }

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                if (_channel != null)
                {
                    await _channel.CloseAsync();
                }
                if (_connection != null)
                {
                    await _connection.CloseAsync();
                }
                _disposed = true;
            }
        }
    }
}
