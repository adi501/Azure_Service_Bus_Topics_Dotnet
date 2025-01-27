﻿using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace Azure_Service_Bus_Topics_Dotnet
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly ITopicClient topicClient;

        public MessagePublisher(ITopicClient topicClient)
        {
            this.topicClient = topicClient;
        }
        public async Task PublisherAsync<T>(T request)
        {
            var message = new Message
            {
                MessageId = Guid.NewGuid().ToString(),
                Body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request))
            };
            message.UserProperties.Add("MessageType", typeof(T).Name);
            await topicClient.SendAsync(message);
        }
    }
}
