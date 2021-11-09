using MediatR;
using System;

namespace Troupon.Core.Application.Ordering.Producers
{
  public class SubmitOrderProducer : IRequest<bool>
  {
    public Guid OrderId { get; set; }

    public SubmitOrderProducer(
      Guid orderId)
    {
      OrderId = orderId;
    }
  }
}
