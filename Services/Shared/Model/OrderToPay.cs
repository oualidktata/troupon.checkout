using System;

namespace Model
{
    public class OrderToPay
    {
      public OrderToPay(
        Guid orderId)
      {
        OrderId = orderId;
        Name = orderId.ToString();
      }

      public Guid OrderId { get; set; }
      public string Name { get; set; }
  }
}
