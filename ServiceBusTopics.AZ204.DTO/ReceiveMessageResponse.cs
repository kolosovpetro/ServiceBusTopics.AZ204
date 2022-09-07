using System;

namespace ServiceBusTopics.AZ204.DTO;

public record ReceiveMessageResponse(DateTime ReceivedAt, Message Message);