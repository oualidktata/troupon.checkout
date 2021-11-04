using MediatR;
using System;
using Troupon.Core.Application.Ordering.DTOs;

namespace Troupon.Core.Application.Ordering.Commands
{
  public class PlaceOrderAsAGuestCommand : IRequest<OrderPlacedDto>
  {
    public Guid Id { get; init; }
  }
}



