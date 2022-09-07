using System;
using System.Text.Json;

namespace ServiceBusTopics.AZ204.DTO;

public class Message
{
    public Message(string createdBy)
    {
        CreatedBy = createdBy;
    }

    public Guid? Id { get; set; }
    public string CreatedBy { get; }
    public DateTime? CreatedAt { get; set; }
    public TopicType? TopicType { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}