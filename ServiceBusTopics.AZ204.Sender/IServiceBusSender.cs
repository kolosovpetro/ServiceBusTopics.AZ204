using System.Threading.Tasks;
using ServiceBusTopics.AZ204.DTO;

namespace ServiceBusTopics.AZ204.Sender;

public interface IServiceBusSender
{
    Task<CreateMessageResponse> SendMessageAsync(Message message, string topicName);
}