using System;

namespace Troupon.Shared.Contracts
{
  public static class EventQueues
  {
    public static readonly string OrderSubmittedEvent = "order-submitted";
    public static readonly string ApprovedOrder = "approved-order";
    public static readonly string OrderExecutionApproved = "order-execution-approved";
    public static readonly string Notification = "notification";

    public static readonly Uri OrderSubmittedEventUri = GetQueueUri(OrderSubmittedEvent);
    public static readonly Uri ApprovedOrderUri = GetQueueUri(ApprovedOrder);
    public static readonly Uri OrderExecutionApprovedUri = GetQueueUri(OrderExecutionApproved);
    public static readonly Uri NotificationUri = GetQueueUri(Notification);

    private static Uri GetQueueUri(string queueName) => new Uri($"exchange:" + queueName);
  }
}
