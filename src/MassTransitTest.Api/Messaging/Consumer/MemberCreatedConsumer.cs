using MassTransit;
using MassTransitTest.Api.Messaging.Publisher;

namespace MassTransitTest.Api.Messaging.Consumer;

public class MemberCreatedConsumer(
    ILogger<MemberCreatedConsumer> logger)
    : IConsumer<MemberCreated>, IConsumerMarker
{
   
    public async Task Consume(ConsumeContext<MemberCreated> context)
    {
        var message = context.Message;

        logger.LogInformation("Received Member Created Event with Id {MemberID}, member name {MemberName}", message.Id, message.FullName);

        try
        {
            logger.LogInformation("Member created with Id {MemberID}, name {MemberName}", message.Id, message.FullName);
        }
        catch (Exception ex)
        {
            logger.LogError("Unable to create member with Id {MemberID}, name {MemberName}", message.Id, message.FullName);
        }
       

        await Task.CompletedTask;
    }
}