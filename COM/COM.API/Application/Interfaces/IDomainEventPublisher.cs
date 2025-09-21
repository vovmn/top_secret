namespace COM.API.Application.Interfaces
{
    public interface IDomainEventPublisher
    {
        ValueTask DisposeAsync();
        Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : class;
    }
}