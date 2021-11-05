using Infra.Common.Models;
using Infra.DomainDrivenDesign.Base;
using MediatR;
using System;

namespace Troupon.Core.Application.Payment.Events
{
  public class GuestOrderPlacedEvent : INotification,
    IDomainEvent,
    IAuditable
  {
    public Guid OrderId { get; set; }
    public DateTime CreationDate { get; set; }
    public string CreatedBy { get; set; }

    public GuestOrderPlacedEvent(
      Guid orderId)
    {
      OrderId = orderId;
      CreationDate = DateTime.UtcNow;
      CreatedBy = "";
    }
  }
}
