using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace Azure_Service_Bus_Topics_Dotnet
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISubscriptionClient subscriptionClient;

        public Worker(ILogger<Worker> logger, ISubscriptionClient subscriptionClient)
        {
            _logger = logger;
            this.subscriptionClient = subscriptionClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            subscriptionClient.RegisterMessageHandler((message, token) =>
            {
                _logger.LogInformation($"message id:{message.MessageId}");
                _logger.LogInformation($"message body:{Encoding.UTF8.GetString(message.Body)}");
                var product = JsonConvert.DeserializeObject<Product>(Encoding.UTF8.GetString(message.Body));
                //Perform operation here ex: DB operation etc
                return subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
            }, new MessageHandlerOptions(ex =>
            {
                _logger.LogError(ex.Exception, ex.Exception.Message);
                return Task.FromException(ex.Exception);
            })
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1
            });
        }
    }
}