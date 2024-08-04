using MassTransit;
using System.Reflection;
using Humanizer;

namespace MassTransitTest.Api.Messaging;

public static class ConfigureServices
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(busConfiguration =>
        {
            busConfiguration.SetKebabCaseEndpointNameFormatter();

            busConfiguration.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqSettings = configuration.GetSection("RabbitMQ");
                var host = rabbitMqSettings.GetValue<string>("Host");
                var username = rabbitMqSettings.GetValue<string>("Username");
                var password = rabbitMqSettings.GetValue<string>("Password");

                cfg.Host(host, h =>
                {
                    h.Username(username!);
                    h.Password(password!);
                });

                RegisterConsumers(busConfiguration, cfg, context, Assembly.GetExecutingAssembly());
            });
        });
        
        return services;
    }
    
    private static void RegisterConsumers(IBusRegistrationConfigurator busConfigurator, IBusFactoryConfigurator busFactoryConfigurator, IBusRegistrationContext context, Assembly assembly)
    {
        var consumerTypes = assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && typeof(IConsumerMarker).IsAssignableFrom(t))
            .ToList();

        foreach (var consumerType in consumerTypes)
        {
            var queueName = GetQueueName(consumerType.Name);
            var topicName = GetTopicName(consumerType.Name);

            busFactoryConfigurator.ReceiveEndpoint(queueName, e =>
            {
                e.ConfigureConsumer(context, consumerType);
                e.ConfigureConsumeTopology = false;

                ((IRabbitMqReceiveEndpointConfigurator)e).Bind(topicName, x =>
                {
                    x.RoutingKey = topicName;
                    x.ExchangeType = "topic";
                });
            });

            busConfigurator.AddConsumer(consumerType);
        }
    }
    
    private static string GetQueueName(string queueName)
    {
        return $"{queueName.Underscore()}_queue";
    }
    
    private static string GetTopicName(string topicName)
    {
        return $"{topicName.Underscore()}_topic";
    }
}