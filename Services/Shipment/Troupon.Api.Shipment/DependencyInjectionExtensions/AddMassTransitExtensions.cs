using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Troupon.Core.Application.Shipment.Consumers;
using Troupon.Shared.Model;

namespace Troupon.Api.Shipment.DependencyInjectionExtensions
{
  public static class AddMassTransitExtensions
  {
    public static IServiceCollection AddMassTransitConfiguration(
      this IServiceCollection services,
      IConfiguration configuration)
    {
      services.AddMassTransit(config =>
      {
        config.AddConsumer<ApprovedOrderEventConsumer>();


        config.UsingRabbitMq((ctx, cfg) =>
        {
          //TODO isolate rabbit mq connection string in appsettings.json
          cfg.Host("amqp://guest:guest@localhost:5672");


         cfg.ReceiveEndpoint(EventQueues.ApprovedOrder, e => {
            e.ConfigureConsumer<ApprovedOrderEventConsumer>(ctx);
          });
        });
      });

      services.AddMassTransitHostedService();

      return services;
    }
  }
}
