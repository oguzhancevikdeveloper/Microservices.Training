using Microservices.Shared.Events.Common;
using Microservices.Shared.Messages;

namespace Microservices.Shared.Events;

public class OrderCreatedEvent : IEvent
{
    public Guid OrderId { get; set; }
    public Guid BuyerId { get; set; }
    public List<OrderItemMessage> OrderItemMessages { get; set; } = default!;
    public decimal TotalPrice { get; set; }
}
