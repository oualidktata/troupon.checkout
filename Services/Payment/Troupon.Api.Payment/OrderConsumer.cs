using MassTransit;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Threading.Tasks;

namespace PaymentService
{
  internal class OrderConsumer : IConsumer<OrderToPay>
  {
    private readonly ILogger<OrderConsumer> _logger;

    public OrderConsumer(ILogger<OrderConsumer> logger)
    {
      _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderToPay> context)
    {
      await Console.Out.WriteLineAsync(context.Message.Name);
      _logger.LogInformation($"Message coming: {context.Message.Name}");

      //Process payment
      //Once payment is successful, publish message to Notification Service

    }
  }
}
