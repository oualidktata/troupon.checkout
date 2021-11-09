using System;
using Infra.DomainDrivenDesign.Base;
using MediatR;

namespace Troupon.Core.Application.Ordering.Events
{
  public class GuestOrderPlacedEvent: IDomainEvent, INotification
  {
    public GuestOrderPlacedEvent(Guid orderId)
    {
      OrderId = orderId;
    }

    public Guid OrderId { get; set; }
    public DateTime CreationDate { get; set; }
  }
}
