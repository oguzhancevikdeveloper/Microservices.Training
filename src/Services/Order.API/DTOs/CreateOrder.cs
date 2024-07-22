namespace Order.API.DTOs;

public record CreateOrder(Guid BuyerId, List<CreateOrderItem> CreateOrderItems);

