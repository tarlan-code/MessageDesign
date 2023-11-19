using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Settings;
using System.Text;

ChannelCreator channelCreator = new();

IModel channel = channelCreator.channel;

channel.QueueDeclare(queue: "P2P",
    durable: false,
    exclusive: false,
    autoDelete: true);

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(queue: "P2P", autoAck: true, consumer);

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();