using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceBusTopics.AZ204.DTO;

namespace ServiceBusTopics.AZ204.Sender.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceBusTopicSenderController : ControllerBase
{
    private const string TopicOne = "topic_one";
    private const string TopicTwo = "topic_two";
    private readonly IServiceBusSender _serviceBusSender;

    public ServiceBusTopicSenderController(IServiceBusSender serviceBusSender)
    {
        _serviceBusSender = serviceBusSender;
    }

    [HttpPost(TopicOne)]
    public async Task<IActionResult> SubmitMessageTopicOneAsync([FromBody] Message message)
    {
        return await RequestAsync(message, TopicOne);
    }

    [HttpPost(TopicTwo)]
    public async Task<IActionResult> SubmitMessageTopicTwoAsync([FromBody] Message message)
    {
        return await RequestAsync(message, TopicTwo);
    }

    [NonAction]
    private async Task<IActionResult> RequestAsync(Message message, string topicName)
    {
        var result = await _serviceBusSender.SendMessageAsync(message, topicName);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}