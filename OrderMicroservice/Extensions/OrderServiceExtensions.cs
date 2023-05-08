using MassTransit;
using OrderMicroservice.Events;

namespace OrderMicroservice.Extensions
{
    public static class OrderServiceExtensions
    {
        public static IServiceCollection AddRabbitMQService(IServiceCollection service)
        {
            service.AddMassTransit(transit =>
            {
                transit.AddConsumer<OrderConsumer>();
                transit.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri("rabbitmq://localhost"), x =>
                    {
                        x.Username("guest");
                        x.Password("guest");
                    });
                    config.ReceiveEndpoint("orderQueue", orderQueue =>
                    {
                        orderQueue.ConfigureConsumer<OrderConsumer>(provider);
                    });
                }));
            });
            return service;
        }
    }
}
