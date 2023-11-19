using RabbitMQ.Client;
using RabbitMQ.Settings;
using System.Text;

ChannelCreator channelCreator = new();

IModel channel = channelCreator.channel;

string queueName = "P2P";

channel.QueueDeclare(queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: true);


byte[] msg = Encoding.UTF8.GetBytes("Hello World");

channel.BasicPublish(exchange: String.Empty,
    body: msg,
    routingKey: queueName);

Console.Read();

