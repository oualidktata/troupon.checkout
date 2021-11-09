using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Troupon.Shared.Contracts;
using Troupon.Shared.Contracts.Events;

namespace Troupon.Api.Payment
{
  public class OrderConsumer : IConsumer<OrderSubmittedEvent>
  {
    private readonly ILogger<OrderConsumer> _logger;

    public OrderConsumer(ILogger<OrderConsumer> logger)
    {
      _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderSubmittedEvent> context)
    {
      // await Console.Out.WriteLineAsync(context.Message.CreationDate.ToString());
      _logger.LogInformation($"Message coming: {context.Message.CreationDate}");

      //Send approved message to Notification Service
      _logger.LogInformation("Sending notification message");
      var notificationEndpoint = await context.GetSendEndpoint(EventQueues.NotificationUri);
      await notificationEndpoint.Send(new NotificationMessage { Content = "Payment successful" });

      //Send approved order event to Shipping Service
      _logger.LogInformation("Sending approved order event");
      var approvedOrderEndpoint = await context.GetSendEndpoint(EventQueues.ApprovedOrderUri);
      await approvedOrderEndpoint.Send(new ApprovedOrderEvent { OrderId = Guid.NewGuid() });
    }
  }
}
