using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Settings;
using System.Text;

ChannelCreator channelCreator = new ChannelCreator();

IModel channel = channelCreator.channel;

string queueName = "WorkQueue";

channel.QueueDeclare(queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false);

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(queue: queueName,
    autoAck: true,
    consumer:  consumer);

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();


