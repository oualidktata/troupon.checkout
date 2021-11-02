using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infra.MediatR.Events;
using Infra.Persistence.Repositories;
using MediatR;
using Troupon.Core.Application.Ordering.Commands;
using Troupon.Core.Application.Ordering.Events;
using Troupon.Core.Domain.Ordering.Entities.Order;

namespace Troupon.Core.Application.Ordering.Handlers.Commands
{
  public class ValidateOrderCommandHandler : IRequestHandler<ValidateOrderCommand, bool>
  {
    private readonly IReadRepository<Order> _orderReadRepo;
    private readonly IWriteRepository<Order> _orderWriteRepo;

    public ValidateOrderCommandHandler(
      IReadRepository<Order> orderReadRepo,
      IWriteRepository<Order> orderWriteRepo)
    {
      _orderReadRepo = orderReadRepo;
      _orderWriteRepo = orderWriteRepo;
    }

    public async Task<bool> Handle(
      ValidateOrderCommand request,
      CancellationToken cancellationToken)
    {
      // Do Validation
      var order = _orderReadRepo.Single(x => x.Id == request.OrderId);
      order.Validated();

      _orderWriteRepo.Update(order);

      await DomainEvents.Raise(new OrderValidatedEvent(request.OrderId));

      return await Task.FromResult(true);
    }
  }
}
