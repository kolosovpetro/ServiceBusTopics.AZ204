using System.Collections.Generic;

namespace ServiceBusTopics.AZ204.DTO;

public static class MessagesStaticLists
{
    public static readonly List<ReceiveMessageResponse> MessagesFromTopicOne = new();
    public static readonly List<ReceiveMessageResponse> MessagesFromTopicTwo = new();
}