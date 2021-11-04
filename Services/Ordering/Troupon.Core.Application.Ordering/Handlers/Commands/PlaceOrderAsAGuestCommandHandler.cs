using MassTransit;
using MediatR;
using Model;
using System.Threading;
using System.Threading.Tasks;
using Troupon.Core.Application.Ordering.Commands;
using Troupon.Core.Application.Ordering.DTOs;

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
      // Publish message to payment service through MassTransit
      // Payment service will receive this message from its end.

      await _publishEndpoint.Publish<OrderToPay>(new OrderToPay(request.Id));

      return await Task.FromResult(new OrderPlacedDto());
    }
  }
}
