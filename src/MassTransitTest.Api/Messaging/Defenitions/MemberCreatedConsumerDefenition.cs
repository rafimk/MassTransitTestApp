using Humanizer;
using MassTransit;
using MassTransitTest.Api.Messaging.Consumer;

namespace MassTransitTest.Api.Messaging.Defenitions;

//public class MemberCreatedConsumerDefenition : ConsumerDefinition<MemberCreatedConsumer>
//{
//    public MemberCreatedConsumerDefenition()
//    {
//        Endpoint(x => x.Name = "MemberCreatedConsumer".Underscore());
//    }
//}
public class MemberCreatedConsumerDefenition : ConsumerDefinition<MemberCreatedConsumer>
{
    public MemberCreatedConsumerDefenition()
    {
        var endpointFormatter = new KebabCaseEndpointNameFormatter(false);
        this.Endpoint(c =>
        {
            c.ConfigureConsumeTopology = false;
            c.Name = "MemberCreatedConsumer".Underscore();
        });
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<MemberCreatedConsumer> consumerConfigurator)
    {
    }
}

