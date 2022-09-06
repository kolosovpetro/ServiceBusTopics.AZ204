using System;

namespace ServiceBusTopics.AZ204.DTO;

public class Message
{
    public Message(string createdBy)
    {
        Id = Guid.NewGuid();
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
    }

    public Guid? Id { get; }
    public string CreatedBy { get; }
    public DateTime? CreatedAt { get; }
}