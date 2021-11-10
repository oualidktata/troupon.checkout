using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Troupon.Core.Application.Ordering.Events;
using Troupon.Core.Application.Ordering.Producers;

namespace Troupon.Core.Application.Ordering.Handlers.Events
{
  public class GuestOrderPlacedEventHandler : INotificationHandler<GuestOrderPlacedEvent>
  {
    private readonly IMediator _mediator;

    public GuestOrderPlacedEventHandler(
      IMediator mediator)
    {
      _mediator = mediator;
    }

    public async Task Handle(
      GuestOrderPlacedEvent @event,
      CancellationToken cancellationToken)
    {
      //Trigger order submission to payment service

      await _mediator.Send(
        new SubmitOrderProducer(@event.OrderId),
        cancellationToken);
    }
  }
}

