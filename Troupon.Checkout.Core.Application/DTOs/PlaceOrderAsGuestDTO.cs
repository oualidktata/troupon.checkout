using System;

namespace Troupon.Checkout.Core.Application.DTOs
{
  public class PlaceOrderAsGuestDto
  {
    public Guid DealId { get; init; }
    public string Email { get; init; }
    public bool SendPromotionalEmails { get; init; }

    public string CardholderName { get; init; }
    public string CardNumber { get; init; }
    public string CardExpirationDate { get; init; }
    public int CVV { get; init; }
  }
}
