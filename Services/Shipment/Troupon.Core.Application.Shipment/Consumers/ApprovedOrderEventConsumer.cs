using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Troupon.Core.Application.Shipment.Events;
using Troupon.Shared.Model;

namespace Troupon.Core.Application.Shipment.Consumers
{
  public class ApprovedOrderEventConsumer: IConsumer<ApprovedOrderEvent>
  {
    private readonly ILogger<ApprovedOrderEventConsumer> _logger;

    public ApprovedOrderEventConsumer(ILogger<ApprovedOrderEventConsumer> logger)
    {
      _logger = logger;
    }


    public async Task Consume(ConsumeContext<ApprovedOrderEvent> context)
    {
      //TODO add logic to start shipment


      // Send message to notification service
      _logger.LogInformation("Sending shipment started notification message");
      var notificationEndpoint = await context.GetSendEndpoint(EventQueues.NotificationUri);
      await notificationEndpoint.Send(new NotificationMessage
      {
        Content = "Shipment started"
      });

      // Send Order Execution Approved event
      _logger.LogInformation("Sending order execution approved");
      var executionStartEndpoint = await context.GetSendEndpoint(EventQueues.NotificationUri);
      await executionStartEndpoint.Send(new OrderExecutionStartedEvent());
    }
  }
}
