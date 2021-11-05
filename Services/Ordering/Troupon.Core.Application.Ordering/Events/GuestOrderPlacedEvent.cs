using Infra.Common.Models;
using Infra.DomainDrivenDesign.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troupon.Core.Application.Ordering.Events
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
