using MassTransit;

namespace MassTransitTest.Api.Messaging.Publisher;

[EntityName("member-created")]
public record MemberCreated(Guid Id, string FullName, string? Email, string MobileNumber) : IEvent;