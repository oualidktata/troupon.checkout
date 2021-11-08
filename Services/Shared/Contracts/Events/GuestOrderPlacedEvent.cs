﻿using System;
using Infra.Common.Models;
using Infra.DomainDrivenDesign.Base;
using MediatR;

namespace Troupon.Shared.Contracts.Events
{
  //Order placed by a user guest event published by OrderingAPI and consumed by PaymentAPI
  public class GuestOrderPlacedEvent: IDomainEvent, IAuditable, INotification
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