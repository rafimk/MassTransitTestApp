namespace MassTransitTest.Api.Messaging.Publisher;

public record MemberCreated(Guid Id, string FullName, string? Email, string MobileNumber) : IEvent;