using System;
using System.Threading;
using System.Threading.Tasks;
using Infra.Common.Models;
using MassTransit;
using MediatR;
using Troupon.Core.Application.Ordering.Events;

namespace Troupon.Core.Application.Ordering.Handlers.Events
{
  public class GuestOrderPlacedEventHandler : INotificationHandler<GuestOrderPlacedEvent>
  {
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;

    public GuestOrderPlacedEventHandler(
      IMediator mediator,
      IPublishEndpoint publishEndpoint)
    {
      _mediator = mediator;
      _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(
      GuestOrderPlacedEvent notification,
      CancellationToken cancellationToken)
    {
      // Publish message to payment service through MassTransit
      // Payment service will receive this message from its end.

      await _publishEndpoint.Publish<GuestOrderPlacedEvent>(new GuestOrderPlacedEvent(notification.OrderId));
    }
  }
}

