using System.Threading;
using System.Threading.Tasks;
using Infra.MediatR.Events;
using MassTransit;
using MediatR;
using Model;
using Troupon.Core.Application.Ordering.Commands;
using Troupon.Core.Application.Ordering.Events;

namespace Troupon.Core.Application.Ordering.Handlers.Commands
{
  public class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand, Unit>
  {
    private readonly IPublishEndpoint _publishEndpoint;

    public ProcessPaymentCommandHandler(IPublishEndpoint publishEndpoint)
    {
      _publishEndpoint = publishEndpoint;
    }

    public async Task<Unit> Handle(
      ProcessPaymentCommand request,
      CancellationToken cancellationToken)
    {
      // Process Payment

      // Publish message to payment service through MassTransit
      // Payment service will receive this message from its end.

      await _publishEndpoint.Publish<OrderToPay>(new OrderToPay(request.OrderId));

      await DomainEvents.Raise(
        new PaymentReceivedEvent(request.OrderId));

      return await Task.FromResult(Unit.Value);
    }
  }
}
