using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Troupon.Api.Payment.Events;
using Troupon.Shared.Model;
using Troupon.Core.Application.Payment.Events;

namespace Troupon.Api.Payment
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
      // await Console.Out.WriteLineAsync(context.Message.CreationDate.ToString());
      // _logger.LogInformation($"Message coming: {context.Message.CreationDate}");

      //Process payment
      //Once payment is successful, publish message to Notification Service

      //Send approved message to Notification Service

      _logger.LogInformation("Sending notification message");
      var notificationEndpoint = await context.GetSendEndpoint(new Uri(EventQueues.Notification));
      await notificationEndpoint.Send(new NotificationMessage { Content = "Payment successful" });

      //Send approved order event to Shipping Service
      _logger.LogInformation("Sending approved order event");
      var approvedOrderEndpoint = await context.GetSendEndpoint(new Uri(EventQueues.ApprovedOrder));
      await approvedOrderEndpoint.Send(new ApprovedOrderEvent { OrderId = Guid.NewGuid() });
    }
  }
}
