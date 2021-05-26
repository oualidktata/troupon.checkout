﻿namespace Troupon.Checkout.Core.Domain.Enums
{
  public enum OrderStatus
  {
    New,
    PendingPayment,
    PaymentApproved,
    PaymentRejected,
    ExecutionApproved,
  }
}
