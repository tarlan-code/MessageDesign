using RabbitMQ.Client;
using RabbitMQ.Settings;
using System.Text;

ChannelCreator channelCreator = new();

IModel channel = channelCreator.channel;

string exchangeName = "Publish/Subscribe";

channel.ExchangeDeclare(exchange: exchangeName,
    type: ExchangeType.Fanout,
    durable: false,
    autoDelete: true);



for (int i = 0; i < 100; i++)
{
    Task.Delay(200);

    byte[] msg = Encoding.UTF8.GetBytes($"Hello World {i}");

    channel.BasicPublish(exchange: exchangeName,
    routingKey: String.Empty,
    body: msg);
}


Console.Read();
