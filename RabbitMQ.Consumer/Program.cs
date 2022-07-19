using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Consumer;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory
{
    Uri = new Uri(AppConstants._rabbitMQURL)
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

FanoutExchangeConsumer.Consume(channel);

Console.ReadKey();