using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Troupon.Api.Ordering.DependencyInjectionExtensions
{
  public static class AddMassTransitExtensions
  {
    public static IServiceCollection AddMassTransitConfiguration(
      this IServiceCollection services,
      IConfiguration configuration)
    {
      services.AddMassTransit(config =>
      {
        config.UsingRabbitMq((ctx, cfg) =>
        {
          //TODO isolate rabbit mq connection string in appsettings.json
          cfg.Host("amqp://guest:guest@localhost:5672");
        });
      });

      services.AddMassTransitHostedService();

      return services;
    }
  }
}
