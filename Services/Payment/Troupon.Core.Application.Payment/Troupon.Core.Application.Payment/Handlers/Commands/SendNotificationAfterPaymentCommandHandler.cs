using MassTransit;
using MediatR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Troupon.Core.Application.Payment.Commands;

namespace Troupon.Core.Application.Payment.Handlers.Commands
{
  class SendNotificationAfterPaymentCommandHandler : IRequestHandler<SendNotificationAfterPaymentCommand>
  {
    private readonly IPublishEndpoint _publishEndpoint;

    public SendNotificationAfterPaymentCommandHandler(
      IPublishEndpoint publishEndpoint
      )
    {
      _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(
      SendNotificationAfterPaymentCommand request,
      CancellationToken cancellationToken)
    {
      // Publish message to payment service through MassTransit
      // Payment service will receive this message from its end.

      await _publishEndpoint.Publish<OrderToPay>(new OrderToPay(request.Id));

      //return await Task.FromResult(new OrderToPay(request.Id));
    }

    Task<Unit> IRequestHandler<SendNotificationAfterPaymentCommand, Unit>.Handle(SendNotificationAfterPaymentCommand request, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
