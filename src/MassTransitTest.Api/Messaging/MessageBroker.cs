using MassTransit;

namespace MassTransitTest.Api.Messaging;

internal sealed class MessageBroker : IMessageBroker
{
    private readonly IBus _bus;

    public MessageBroker(IBus bus)
    {
        _bus = bus;
    }

    public async Task SendAsync<T>(T message, CancellationToken cancellationToken = default) where T : IMessage
    {
        await _bus.Publish(message, cancellationToken);
    }
}