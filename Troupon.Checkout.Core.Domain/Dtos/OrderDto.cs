using System;
using Troupon.Core.Domain.Ordering.Enums;

namespace Troupon.Core.Domain.Ordering.Dtos
{
  public class OrderDto
  {
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public OrderStatus Status { get; set; }
  }
}
