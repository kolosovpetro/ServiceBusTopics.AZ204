using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceBusTopics.AZ204.DTO;

namespace ServiceBusTopics.AZ204.Consumer;

public class TopicOneBackgroundService : IHostedService, IDisposable
{
    private readonly IMessageConsumer _messageConsumer;
    private readonly ILogger<ReceiveMessageResponse> _logger;
    private const string TopicPath = "topic_one";
    private const string SubscriptionName = "topic_one_subscription";

    public TopicOneBackgroundService(IMessageConsumer messageConsumer, ILogger<ReceiveMessageResponse> logger)
    {
        _messageConsumer = messageConsumer;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Background service consumer of topic ONE has been started.");
        await _messageConsumer.RegisterHandlersAndReceiveMessagesAsync(TopicPath, SubscriptionName);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping background service consumer of topic ONE...");
        await _messageConsumer.CloseQueueAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual async void Dispose(bool disposing)
    {
        if (disposing)
        {
            await _messageConsumer.DisposeAsync().ConfigureAwait(false);
        }
    }
}