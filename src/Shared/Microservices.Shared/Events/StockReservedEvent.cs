using Microservices.Shared.Events.Common;

namespace Microservices.Shared.Events;

public class StockReservedEvent : IEvent
{
    public Guid BuyerId { get; set; }
    public Guid OrderId { get; set; }
    public decimal TotalPrice { get; set; }
}
