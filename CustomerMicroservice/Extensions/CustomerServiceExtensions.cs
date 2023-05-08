using MassTransit;

namespace CustomerMicroservice.Extensions
{
    public static class CustomerServiceExtensions
    {
        public static IServiceCollection AddRabbitMQService(IServiceCollection service)
        {
            service.AddMassTransit(transit =>
            {
                transit.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                }));
            });
            return service;
        }
    }
}
