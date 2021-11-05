using System;

namespace Troupon.Api.Payment.Events
{
  public class ApprovedOrderEvent
  {
     public Guid OrderId { get; set; }
  }
}
