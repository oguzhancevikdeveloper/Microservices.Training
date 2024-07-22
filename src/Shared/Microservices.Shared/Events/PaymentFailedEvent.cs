using Microservices.Shared.Events.Common;

namespace Microservices.Shared.Events;

public class PaymentFailedEvent : IEvent
{
    public Guid OrderId { get; set; }
    public string Message { get; set; } = default!;
}
