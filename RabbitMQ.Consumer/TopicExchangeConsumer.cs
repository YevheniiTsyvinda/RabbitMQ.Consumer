using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    internal class TopicExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare(AppConstants._topicExchange, ExchangeType.Topic);

            channel.QueueDeclare(AppConstants._topicQueue,
           durable: true,
           exclusive: false,
           autoDelete: false,
           arguments: null);
            channel.QueueBind(AppConstants._topicQueue, AppConstants._topicExchange, "account.*");
            channel.BasicQos(0, 10, false);


            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume(AppConstants._directExchange, true, consumer);

            Console.WriteLine("Consumer started");
        }
    }
}
