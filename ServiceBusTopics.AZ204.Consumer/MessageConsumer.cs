using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using ServiceBusTopics.AZ204.DTO;

namespace ServiceBusTopics.AZ204.Consumer;

public class MessageConsumer : IMessageConsumer
{
    private readonly ILogger<ReceiveMessageResponse> _logger;
    private const string EnvironmentKey = "SERVICE_BUS_CONNECTION_STRING";
    private readonly ServiceBusClient _client;
    private ServiceBusProcessor _processor;

    public MessageConsumer(ILogger<ReceiveMessageResponse> logger)
    {
        _logger = logger;

        var connectionString = Environment.GetEnvironmentVariable(EnvironmentKey) ??
                               throw new InvalidOperationException(
                                   $"Environment variable is not set {EnvironmentKey}");

        _client = new ServiceBusClient(connectionString);
    }

    public async Task RegisterHandlersAndReceiveMessagesAsync(string topicPath, string subscriptionName)
    {
        var serviceBusProcessorOptions = new ServiceBusProcessorOptions
        {
            MaxConcurrentCalls = 1,
            AutoCompleteMessages = false,
        };

        _processor = _client.CreateProcessor(topicPath, subscriptionName, serviceBusProcessorOptions);
        _processor.ProcessMessageAsync += ProcessMessagesAsync;
        _processor.ProcessErrorAsync += ProcessErrorAsync;

        await _processor.StartProcessingAsync().ConfigureAwait(false);
    }

    private Task ProcessErrorAsync(ProcessErrorEventArgs arg)
    {
        _logger.LogError(arg.Exception, "Message handler encountered an exception");
        _logger.LogDebug($"- ErrorSource: {arg.ErrorSource}");
        _logger.LogDebug($"- Entity Path: {arg.EntityPath}");
        _logger.LogDebug($"- FullyQualifiedNamespace: {arg.FullyQualifiedNamespace}");

        return Task.CompletedTask;
    }

    private async Task ProcessMessagesAsync(ProcessMessageEventArgs args)
    {
        var message = args.Message.Body.ToObjectFromJson<Message>();

        var response = new ReceiveMessageResponse(DateTime.UtcNow, message);

        switch (message.TopicType)
        {
            case TopicType.TopicOne:
                MessagesStaticLists.MessagesFromTopicOne.Add(response);
                break;
            case TopicType.TopicTwo:
                MessagesStaticLists.MessagesFromTopicTwo.Add(response);
                break;
            default:
                _logger.LogInformation($"Topic type was out of range {(int)message.TopicType}.");
                break;
        }

        _logger.LogInformation($"Message Received: {response}");

        await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);
    }

    public async Task CloseQueueAsync()
    {
        await _processor.CloseAsync().ConfigureAwait(false);
    }

    public async ValueTask DisposeAsync()
    {
        if (_processor != null)
        {
            await _processor.DisposeAsync().ConfigureAwait(false);
        }

        if (_client != null)
        {
            await _client.DisposeAsync().ConfigureAwait(false);
        }
    }
}