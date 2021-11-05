using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Troupon.Core.Application.Payment.Events;

namespace PaymentService
{
  internal class OrderConsumer : IConsumer<GuestOrderPlacedEvent>
  {
    private readonly ILogger<OrderConsumer> _logger;

    public OrderConsumer(ILogger<OrderConsumer> logger)
    {
      _logger = logger;
    }

    public async Task Consume(ConsumeContext<GuestOrderPlacedEvent> context)
    {
      await Console.Out.WriteLineAsync(context.Message.CreationDate.ToString());
      _logger.LogInformation($"Message coming: {context.Message.CreationDate}");

      //Process payment
      //Once payment is successful, publish message to Notification Service
    }
  }
}
