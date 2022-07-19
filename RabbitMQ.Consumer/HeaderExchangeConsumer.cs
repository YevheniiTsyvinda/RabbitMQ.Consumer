using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer;

internal class HeaderExchangeConsumer
{
    public static void Consume(IModel channel)
    {
        channel.ExchangeDeclare(AppConstants._headerExchange, ExchangeType.Headers);

        channel.QueueDeclare(AppConstants._headerQueue,
           durable: true,
           exclusive: false,
           autoDelete: false,
           arguments: null);

        var header = new Dictionary<string, object> {
            {"account","update" }
        };

        channel.QueueBind(AppConstants._headerQueue, AppConstants._headerExchange, String.Empty, header);
        channel.BasicQos(0, 10, false);


        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, e) =>
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(message);
        };

        channel.BasicConsume(AppConstants._headerQueue, true, consumer);

        Console.WriteLine("Consumer started");
    }
}
