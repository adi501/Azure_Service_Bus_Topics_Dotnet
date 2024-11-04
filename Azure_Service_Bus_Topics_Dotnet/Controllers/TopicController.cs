using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Azure_Service_Bus_Topics_Dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly IMessagePublisher messagePublisher;

        public TopicController(IMessagePublisher messagePublisher)
        {
            this.messagePublisher = messagePublisher;
        }

        // POST api/values
        [HttpPost(template: "product")]
        public async Task SendToProduct([FromBody] Product product)
        {
            await messagePublisher.PublisherAsync(product);
        }

        [HttpPost(template: "order")]
        public async Task SentToOrder([FromBody] Order order)
        {
            await messagePublisher.PublisherAsync(order);
        }
    }
}
