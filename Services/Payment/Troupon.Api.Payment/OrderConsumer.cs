using Infra.Common.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace PaymentService
{
  internal class OrderConsumer : IConsumer<IAuditable>
  {
    private readonly ILogger<OrderConsumer> _logger;

    public OrderConsumer(ILogger<OrderConsumer> logger)
    {
      _logger = logger;
    }

    public async Task Consume(ConsumeContext<IAuditable> context)
    {
      await Console.Out.WriteLineAsync(context.Message.CreationDate.ToString());
      _logger.LogInformation($"Message coming: {context.Message.CreationDate}");

      //Process payment
      //Once payment is successful, publish message to Notification Service

    }
  }
}
