using MassTransit;
using Shared.Models;

namespace OrderMicroservice.Events
{
    public class OrderConsumer : IConsumer<Order>
    {
        public async Task Consume(ConsumeContext<Order> context)
        {
            var obj = context.Message;
        }
    }
}
