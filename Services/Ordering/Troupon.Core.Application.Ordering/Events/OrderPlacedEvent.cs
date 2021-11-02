using System;
using Infra.DomainDrivenDesign.Base;
using MediatR;

namespace Troupon.Core.Application.Ordering.Events
{
  public class OrderPlacedEvent : INotification,
    IDomainEvent
  {
    public Guid OrderId { get; set; }
    public DateTime CreationDate { get; set; }

    public OrderPlacedEvent(
      Guid orderId)
    {
      OrderId = orderId;
      CreationDate = DateTime.UtcNow;
    }
  }
}
