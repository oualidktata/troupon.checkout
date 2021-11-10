using System;
using MediatR;

namespace Troupon.Core.Application.Ordering.Commands
{
  public class ProcessPaymentCommand : IRequest<Unit>
  {
    public Guid OrderId { get; set; }

    public ProcessPaymentCommand(
      Guid orderId)
    {
      OrderId = orderId;
    }
  }
}
