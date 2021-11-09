using MassTransit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Troupon.Core.Application.Ordering.Producers;
using Troupon.Shared.Contracts;
using Troupon.Shared.Contracts.Events;

namespace Troupon.Core.Application.Ordering.Handlers.Producers
{
  public class SubmitOrderProducerHandler : IRequestHandler<SubmitOrderProducer, bool>
  {
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public SubmitOrderProducerHandler(
      ISendEndpointProvider sendEndpointProvider)
    {
      _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task<bool> Handle(
      SubmitOrderProducer notification,
      CancellationToken cancellationToken)
    {
      // Publish message to payment service through MassTransit
      // Payment service will receive this message from its end.

      var orderPlacedEndpoint = await _sendEndpointProvider.GetSendEndpoint(EventQueues.OrderSubmittedEventUri);
      await orderPlacedEndpoint.Send(new OrderSubmittedEvent(notification.OrderId), cancellationToken);

      return await Task.FromResult(true);
    }
  }
}
