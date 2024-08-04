using Humanizer;
using MassTransit;

namespace MassTransitTest.Api.Messaging;

public class CustomEntityNameFormatter : IEntityNameFormatter
{
    public string FormatEntityName<T>()
    {
        return typeof(T).Name.Underscore();
    }
}