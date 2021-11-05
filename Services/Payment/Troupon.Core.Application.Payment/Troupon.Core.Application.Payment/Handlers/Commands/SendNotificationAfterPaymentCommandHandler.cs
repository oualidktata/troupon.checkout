using MassTransit;
using MediatR;
using Model;
using System.Threading;
using System.Threading.Tasks;
using Troupon.Core.Application.Payment.Commands;
using Troupon.Core.Application.Payment.DTOs;

namespace Troupon.Core.Application.Payment.Handlers.Commands
{
  class SendNotificationAfterPaymentCommandHandler : IRequestHandler<SendNotificationAfterPaymentCommand, PaymentReceivedDto>
  {
    private readonly IPublishEndpoint _publishEndpoint;

    public SendNotificationAfterPaymentCommandHandler(
      IPublishEndpoint publishEndpoint
      )
    {
      _publishEndpoint = publishEndpoint;
    }

    public async Task<PaymentReceivedDto> Handle(
      SendNotificationAfterPaymentCommand request,
      CancellationToken cancellationToken)
    {
      // Publish message to payment service through MassTransit
      // Payment service will receive this message from its end.

      await _publishEndpoint.Publish<OrderToPay>(new OrderToPay(request.Id));

      return await Task.FromResult(new PaymentReceivedDto());
    }
  }
}
