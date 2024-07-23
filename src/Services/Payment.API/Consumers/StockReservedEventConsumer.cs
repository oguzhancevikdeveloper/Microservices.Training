using MassTransit;
using Microservices.Shared.Events;

namespace Payment.API.Consumers;

public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
{
    readonly IPublishEndpoint _publishEndpoint;

    public StockReservedEventConsumer(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task Consume(ConsumeContext<StockReservedEvent> context)
    {
        Random random = new();
        int number = random.Next(0, 20);
        if (number % 2 == 0)
        {
            PaymentCompletedEvent paymentCompletedEvent = new()
            {
                OrderId = context.Message.OrderId
            };
            _publishEndpoint.Publish(paymentCompletedEvent);

            Console.WriteLine("Ödeme başarılı...");
        }
        else
        {
            PaymentFailedEvent paymentFailedEvent = new()
            {
                OrderId = context.Message.OrderId,
                Message = "Bakiye yetersiz..."
            };

            _publishEndpoint.Publish(paymentFailedEvent);

            Console.WriteLine("Ödeme başarısız...");
        }

        return Task.CompletedTask;
    }
}