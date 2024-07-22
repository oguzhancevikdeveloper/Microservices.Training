namespace Microservices.Shared.Messages;

public class OrderItemMessage
{
    public string ProductId { get; set; } = default!;
    public int Count { get; set; }
}
