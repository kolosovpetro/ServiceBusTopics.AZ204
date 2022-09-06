using System;

namespace ServiceBusTopics.AZ204.DTO;

public record CreateMessageResponse(Guid? MessageId, bool Success, DateTime? CreatedAt, string MessageBody);