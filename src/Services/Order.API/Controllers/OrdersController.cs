using MassTransit;
using Microservices.Shared.Events;
using Microsoft.AspNetCore.Mvc;
using Order.API.DTOs;
using Order.API.Models;
using Order.API.Models.Entities;

namespace Order.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(OrderAPIDbContext _orderAPIDbContext, IPublishEndpoint _publishEndpoint) : ControllerBase
{
    [HttpPost]

    public async Task<IActionResult> CreateOrder(CreateOrder createOrder, CancellationToken cancellationToken)
    {
        Models.Entities.Order order = new()
         {
             OrderId = Guid.NewGuid(),
             BuyerId = createOrder.BuyerId,
             CreatedDate = DateTime.Now,
             OrderStatus = Models.Enums.OrderStatus.Suspend
         };

        order.OrderItems = createOrder.CreateOrderItems.Select(oi => new OrderItem
        {
            Count = oi.Count,
            Price = oi.Price,
            ProductId = oi.ProductId
        }).ToList();

        order.TotalPrice = createOrder.CreateOrderItems.Sum(oi => (oi.Count * oi.Price));

        await _orderAPIDbContext.Orders.AddAsync(order);
        await _orderAPIDbContext.SaveChangesAsync(cancellationToken);

        OrderCreatedEvent orderCreatedEvent = new()
        {
            BuyerId = order.BuyerId,
            OrderId = order.OrderId,
            TotalPrice = order.TotalPrice,
            OrderItemMessages = order.OrderItems.Select(oi => new Microservices.Shared.Messages.OrderItemMessage
            {
                Count = oi.Count,
                ProductId = oi.ProductId,
            }).ToList(),
        };

        await _publishEndpoint.Publish(orderCreatedEvent, cancellationToken);

        return Ok();
    }
}
