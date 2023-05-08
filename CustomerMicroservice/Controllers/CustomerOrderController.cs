using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace CustomerMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private readonly IBus bus;
        public CustomerOrderController(IBus bus)
        {
            this.bus = bus;
        }

        [HttpPost]
        public async Task<string> CreateOrder(Order order)
        {
            if (order is not null)
            {
                order.OrderDate = DateTime.Now;
                var uri = new Uri("rabbitmq://localhost:5672/orderQueue");
                var endPoint = await this.bus.GetSendEndpoint(uri);
                Console.WriteLine(endPoint);
                await endPoint.Send(order);
                return "sent to order queue";
            }
            return "failed to send";
        }
    }
}
