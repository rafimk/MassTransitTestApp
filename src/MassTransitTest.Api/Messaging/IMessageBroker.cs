namespace MassTransitTest.Api.Messaging;

public interface IMessageBroker
{
    Task SendAsync<T>(T message, CancellationToken cancellationToken = default) where T : IMessage;
}