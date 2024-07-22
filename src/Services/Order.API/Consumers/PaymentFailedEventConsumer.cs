using MassTransit;
using Microservices.Shared.Events;
using Microsoft.EntityFrameworkCore;
using Order.API.Models;

namespace Order.API.Consumers;

public class PaymentFailedEventConsumer(OrderAPIDbContext _orderAPIDbContext) : IConsumer<PaymentFailedEvent>
{
    public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
    {
        Models.Entities.Order? order = await _orderAPIDbContext.Orders.FirstOrDefaultAsync(x => x.OrderId.Equals(context.Message.OrderId));
        order.OrderStatus = Models.Enums.OrderStatus.Failed;
        await _orderAPIDbContext.SaveChangesAsync();
;
    }
}
