using MassTransit;
using Microservices.Shared.Events;
using Microsoft.EntityFrameworkCore;
using Order.API.Models;

namespace Order.API.Consumers;

public class StockNotReservedEventConsumer(OrderAPIDbContext _orderAPIDbContext) : IConsumer<StockNotReservedEvent>
{
    public async Task Consume(ConsumeContext<StockNotReservedEvent> context)
    {
        Models.Entities.Order? order = await _orderAPIDbContext.Orders.SingleOrDefaultAsync(x => x.OrderId.Equals(context.Message.OrderId));
        order.OrderStatus = Models.Enums.OrderStatus.Failed;
        await _orderAPIDbContext.SaveChangesAsync();
    }
}
