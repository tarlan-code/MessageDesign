
using RabbitMQ.Client;
using RabbitMQ.Settings;
using System.Text;

ChannelCreator channelCreator = new ChannelCreator();

IModel channel = channelCreator.channel;

string queueName = "WorkQueue";

channel.QueueDeclare(queue: queueName,
    durable: false,
    exclusive:false,
    autoDelete:false);

IBasicProperties basicProperties = channel.CreateBasicProperties();

basicProperties.Persistent = true;

for (int i = 0; i < 100; i++)
{
    Task.Delay(200).Wait();    

    byte[] msg = Encoding.UTF8.GetBytes($"Salam-{i}");

    channel.BasicPublish(exchange: String.Empty,
        routingKey: queueName,
        body: msg,
        basicProperties: basicProperties);
}

Console.Read();

