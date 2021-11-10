using Infra.MediatR.Events;
using MassTransit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Troupon.Core.Application.Ordering.Commands;
using Troupon.Core.Application.Ordering.DTOs;
using Troupon.Core.Application.Ordering.Events;

namespace Troupon.Core.Application.Ordering.Handlers.Commands
{
  public class PlaceOrderAsAGuestCommandHandler : IRequestHandler<PlaceOrderAsAGuestCommand, OrderPlacedDto>
  {
    private readonly IPublishEndpoint _publishEndpoint;

    public PlaceOrderAsAGuestCommandHandler(
      IPublishEndpoint publishEndpoint
      )
    {
      _publishEndpoint = publishEndpoint;
    }

    public async Task<OrderPlacedDto> Handle(
      PlaceOrderAsAGuestCommand request,
      CancellationToken cancellationToken)
    {
      //Convert orderPlaced with and event and raise it to be handled in OrderPlacedEventHandler

      await DomainEvents.Raise(new GuestOrderPlacedEvent(request.Id));

      return await Task.FromResult(new OrderPlacedDto
      {
        Id = request.Id,
      });
    }
  }
}
