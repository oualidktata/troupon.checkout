using Infra.Common.Models;
using System;

namespace Troupon.Shared.Contracts.Events
{
  public class OrderSubmittedEvent : IAuditable
  {
    //Order submitted by a user guest event published by OrderingAPI and consumed by PaymentAPI
    public Guid OrderId { get; set; }
    public DateTime CreationDate { get; set; }
    public string CreatedBy { get; set; }

    public OrderSubmittedEvent(
      Guid orderId)
    {
      OrderId = orderId;
      CreationDate = DateTime.UtcNow;
      CreatedBy = "";
    }
  }
}
