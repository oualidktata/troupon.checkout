using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Troupon.Core.Application.Ordering.Commands;
using Troupon.Core.Application.Ordering.Events;

namespace Troupon.Core.Application.Ordering.Handlers.Events
{
  public class OrderPlacedEventHandler : INotificationHandler<OrderPlacedEvent>
  {
    private readonly IMediator _mediator;

    public OrderPlacedEventHandler(
      IMediator mediator)
    {
      _mediator = mediator;
    }

    public async Task Handle(
      OrderPlacedEvent notification,
      CancellationToken cancellationToken)
    {
      // Trigger Validation

      await _mediator.Send(
        new ValidateOrderCommand(notification.OrderId),
        cancellationToken);
    }
  }
}
