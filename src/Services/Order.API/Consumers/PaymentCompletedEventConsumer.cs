using MassTransit;
using Microservices.Shared.Events;
using Microsoft.EntityFrameworkCore;
using Order.API.Models;

namespace Order.API.Consumers;

public class PaymentCompletedEventConsumer(OrderAPIDbContext _orderAPIDbContext) : IConsumer<PaymentCompletedEvent>
{
    public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
    {
        Models.Entities.Order? order = await _orderAPIDbContext.Orders.FirstOrDefaultAsync(o => o.OrderId.Equals(context.Message.OrderId));
        order.OrderStatus = Models.Enums.OrderStatus.Completed;
        await _orderAPIDbContext.SaveChangesAsync();

    }
}
