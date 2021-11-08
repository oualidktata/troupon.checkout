using System;

// Approved Order event sent when a payment is approved, published from Payment API and consumed by Shipment API
namespace Troupon.Shared.Contracts.Events
{
  public class ApprovedOrderEvent
  {
    public Guid OrderId { get; set; }
  }
}
