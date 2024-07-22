using Microservices.Shared.Events.Common;

namespace Microservices.Shared.Events;

public class PaymentCompletedEvent : IEvent
{
    public Guid OrderId { get; set; }
}
