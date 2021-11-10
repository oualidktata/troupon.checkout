using MediatR;
using System;
using Troupon.Core.Application.Payment.DTOs;

namespace Troupon.Core.Application.Payment.Commands
{
    public class SendNotificationAfterPaymentCommand : IRequest<PaymentReceivedDto>
    {
      public Guid Id { get; init; }
    }
}
