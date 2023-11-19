using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Settings;
using System.Text;

ChannelCreator channelCreator = new();

IModel channel = channelCreator.channel;

string queueName = "Request-Repsonse-Pattern";

channel.QueueDeclare(queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: true);

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(queue: queueName,
    autoAck: true,
    consumer:  consumer);


consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

    Task.Delay(5000).Wait(); //Loading ....

    byte[] msg = Encoding.UTF8.GetBytes("Proses tamamlandi");

    IBasicProperties properties = e.BasicProperties;
    IBasicProperties replyProperties = channel.CreateBasicProperties();

    replyProperties.CorrelationId = properties.CorrelationId;

    channel.BasicPublish(exchange: String.Empty,
        routingKey: properties.ReplyTo,
        basicProperties: replyProperties,
        body: msg);

};

Console.Read();
