using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Troupon.Core.Application.Ordering.Commands;
using Troupon.Core.Application.Ordering.Events;

namespace Troupon.Core.Application.Ordering.Handlers.Events
{
  public class OrderValidatedEventHandler : INotificationHandler<OrderValidatedEvent>
  {
    private readonly IMediator _mediator;

    public OrderValidatedEventHandler(
      IMediator mediator)
    {
      _mediator = mediator;
    }

    public async Task Handle(
      OrderValidatedEvent notification,
      CancellationToken cancellationToken)
    {
      await _mediator.Send(
        new ProcessPaymentCommand(notification.OrderId),
        cancellationToken);
    }
  }
}
