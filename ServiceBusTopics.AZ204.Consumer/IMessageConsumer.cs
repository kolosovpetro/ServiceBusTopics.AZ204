using System.Threading.Tasks;

namespace ServiceBusTopics.AZ204.Consumer;

public interface IMessageConsumer
{
    Task RegisterHandlersAndReceiveMessagesAsync(string topicPath, string subscriptionName);
    ValueTask DisposeAsync();
    Task CloseQueueAsync();
}