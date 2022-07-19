using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer;

internal class DirectExchangeConsumer
{
    public static void Consume(IModel channel)
    {
        channel.ExchangeDeclare(AppConstants._directExchange, ExchangeType.Direct);

        channel.QueueDeclare(AppConstants._directQueue,
       durable: true,
       exclusive: false,
       autoDelete: false,
       arguments: null);
        channel.QueueBind(AppConstants._directQueue, AppConstants._directExchange, AppConstants._accountInit);
        channel.BasicQos(0, 10, false);


        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, e) =>
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(message);
        };

        channel.BasicConsume(AppConstants._directQueue, true, consumer);

        Console.WriteLine("Consumer started");
    }
}
