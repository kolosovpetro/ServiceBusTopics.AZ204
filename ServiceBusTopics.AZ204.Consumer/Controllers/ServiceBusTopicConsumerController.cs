using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceBusTopics.AZ204.DTO;

namespace ServiceBusTopics.AZ204.Consumer.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceBusTopicConsumerController : ControllerBase
{
    [HttpGet("topic_one")]
    public async Task<IActionResult> ReadTopicOneMessagesAsync()
    {
        var messages = MessagesStaticLists.MessagesFromTopicOne;

        return await Task.FromResult(Ok(messages));
    }

    [HttpGet("topic_two")]
    public async Task<IActionResult> ReadTopicTwoMessagesAsync()
    {
        var messages = MessagesStaticLists.MessagesFromTopicTwo;

        return await Task.FromResult(Ok(messages));
    }
}