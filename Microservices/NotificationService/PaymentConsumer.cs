using MassTransit;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Threading.Tasks;

namespace NotificationService
{
  internal class PaymentConsumer : IConsumer<PaymentConfirmation>
  {
    private readonly ILogger<PaymentConfirmation> _logger;

    public PaymentConsumer(ILogger<PaymentConfirmation> logger)
    {
      _logger = logger;
    }

    public async Task Consume(ConsumeContext<PaymentConfirmation> context)
    {
      await Console.Out.WriteLineAsync(context.Message.IsPaymentReceived.ToString());

      _logger.LogInformation($"Message coming: {context.Message.IsPaymentReceived}");
    }
  }
}
