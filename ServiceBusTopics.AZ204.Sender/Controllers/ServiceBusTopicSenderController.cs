using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceBusTopics.AZ204.DTO;

namespace ServiceBusTopics.AZ204.Sender.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceBusTopicSenderController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SubmitMessageAsync([FromBody] Message message)
    {
        return await Task.FromResult(Ok(message));
    }
}