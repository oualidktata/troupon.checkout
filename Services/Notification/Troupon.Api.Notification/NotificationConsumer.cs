using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Troupon.Shared.Model;

namespace Troupon.Api.Notification
{
  public class NotificationConsumer : IConsumer<NotificationMessage>
  {
    private readonly ILogger<NotificationMessage> _logger;

    public NotificationConsumer(ILogger<NotificationMessage> logger)
    {
      _logger = logger;
    }

    public async Task Consume(ConsumeContext<NotificationMessage> context)
    {
      var content = context.Message.Content;
      await Console.Out.WriteLineAsync(content);

      _logger.LogInformation($"Message coming: {content}");
    }
  }
}
