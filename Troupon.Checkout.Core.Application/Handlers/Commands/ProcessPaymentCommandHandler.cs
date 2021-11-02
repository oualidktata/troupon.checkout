using System.Threading;
using System.Threading.Tasks;
using Infra.MediatR.Events;
using MediatR;
using Troupon.Core.Application.Ordering.Commands;
using Troupon.Core.Application.Ordering.Events;

namespace Troupon.Core.Application.Ordering.Handlers.Commands
{
  public class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand, Unit>
  {
    public async Task<Unit> Handle(
      ProcessPaymentCommand request,
      CancellationToken cancellationToken)
    {
      // Process Payment

      await DomainEvents.Raise(
        new PaymentReceivedEvent(request.OrderId));

      return await Task.FromResult(Unit.Value);
    }
  }
}
