using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer;

internal class FanoutExchangeConsumer
{
    public static void Consume(IModel channel)
    {
        channel.ExchangeDeclare(AppConstants._fanoutExchange, ExchangeType.Fanout);

        channel.QueueDeclare(AppConstants._fanoutQueue,
           durable: true,
           exclusive: false,
           autoDelete: false,
           arguments: null);

        var header = new Dictionary<string, object> {
            {"account","update" }
        };

        channel.QueueBind(AppConstants._fanoutQueue, AppConstants._fanoutExchange, String.Empty);
        channel.BasicQos(0, 10, false);


        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, e) =>
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(message);
        };

        channel.BasicConsume(AppConstants._fanoutQueue, true, consumer);

        Console.WriteLine("Consumer started");
    }
}
