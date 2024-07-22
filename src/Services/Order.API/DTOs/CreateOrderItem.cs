namespace Order.API.DTOs;

public record CreateOrderItem(string ProductId, int Count, decimal Price);
