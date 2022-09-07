using System;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using ServiceBusTopics.AZ204.DTO;

namespace ServiceBusTopics.AZ204.Sender;

public class ServiceBusSender : IServiceBusSender
{
    private const string EnvironmentKey = "SERVICE_BUS_CONNECTION_STRING";
    private readonly ILogger<CreateMessageResponse> _logger;
    private readonly ServiceBusClient _serviceBusClient;

    public ServiceBusSender(ILogger<CreateMessageResponse> logger)
    {
        _logger = logger;

        var connectionString = Environment.GetEnvironmentVariable(EnvironmentKey) ??
                               throw new InvalidOperationException(
                                   $"Environment variable is not set {EnvironmentKey}");

        _serviceBusClient = new ServiceBusClient(connectionString);
    }

    public async Task<CreateMessageResponse> SendMessageAsync(Message message, string topicName)
    {
        var sender = _serviceBusClient.CreateSender(topicName);
        var messageBody = JsonSerializer.Serialize(message);
        var serviceBusMessage = new ServiceBusMessage(messageBody);

        serviceBusMessage.ApplicationProperties.Add("id", message.Id);

        try
        {
            await sender.SendMessageAsync(serviceBusMessage).ConfigureAwait(false);
            _logger.LogInformation($"Message has been sent: {messageBody} to the topic: {message.TopicType}");
            return new CreateMessageResponse(message.Id, true, DateTime.Now, messageBody);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);

            return new CreateMessageResponse(
                MessageId: Guid.Empty,
                Success: false,
                CreatedAt: DateTime.Now,
                MessageBody: "ERROR_DURING_CREATION_OF_MESSAGE");
        }
    }
}