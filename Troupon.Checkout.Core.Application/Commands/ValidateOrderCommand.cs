using System;
using MediatR;

namespace Troupon.Core.Application.Ordering.Commands
{
  public class ValidateOrderCommand : IRequest<bool>
  {
    public Guid OrderId { get; set; }

    public ValidateOrderCommand(
      Guid orderId)
    {
      OrderId = orderId;
    }
  }
}
