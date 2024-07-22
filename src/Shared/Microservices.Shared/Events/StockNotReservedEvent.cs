using Microservices.Shared.Events.Common;

namespace Microservices.Shared.Events;

public class StockNotReservedEvent : IEvent
{
    public Guid BuyerId { get; set; }
    public Guid OrderId { get; set; }
    public string Message { get; set; } = default!;
}
