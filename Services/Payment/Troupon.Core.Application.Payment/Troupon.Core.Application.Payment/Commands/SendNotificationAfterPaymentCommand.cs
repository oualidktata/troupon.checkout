using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troupon.Core.Application.Payment.Commands
{
    public class SendNotificationAfterPaymentCommand : IRequest
    {
      public Guid Id { get; init; }
    }
}
