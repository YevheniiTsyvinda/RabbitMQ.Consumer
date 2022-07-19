using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

const string _rabbitMQURL = "amqp://guest:guest@localhost:5672";
const string _queueName = "demo-queue";


var factory = new ConnectionFactory
{
    Uri = new Uri(_rabbitMQURL)
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare(_queueName,
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (sender, e) =>
{
    var body = e.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
};

channel.BasicConsume(_queueName,true,consumer);

Console.ReadKey();