using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Troupon.Shared.Contracts;
using Troupon.Shared.Contracts.Events;

namespace Troupon.Core.Application.Ordering.Handlers.Events
{
  public class GuestOrderPlacedEventHandler : INotificationHandler<GuestOrderPlacedEvent>
  {
    private readonly IMediator _mediator;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public GuestOrderPlacedEventHandler(
      IMediator mediator,
      ISendEndpointProvider sendEndpointProvider)
    {
      _mediator = mediator;
      _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task Handle(
      GuestOrderPlacedEvent notification,
      CancellationToken cancellationToken)
    {
      // Publish message to payment service through MassTransit
      // Payment service will receive this message from its end.

      try
      {
        var guestOrderPlacedEndpoint = await _sendEndpointProvider.GetSendEndpoint(EventQueues.GuestOrderPlacedEventUri);
        await guestOrderPlacedEndpoint.Send(new GuestOrderPlacedEvent(notification.OrderId), cancellationToken);
      }
      catch(Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}

