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


string replyQueueName = channel.QueueDeclare().QueueName;

string correlationId = Guid.NewGuid().ToString();

#region Request


IBasicProperties properties = channel.CreateBasicProperties();  

properties.CorrelationId = correlationId;  //Consumer-dan gelem response-un heqiqeten bu publisherin gonderdiyi mesaj oldugunu bilmek uchun 

properties.ReplyTo = replyQueueName;  //Consumer hansi queue ya mesaji respons olaraq gonderecek  


byte[] msg = Encoding.UTF8.GetBytes("Hello World");

channel.BasicPublish(exchange: String.Empty,
    routingKey: queueName,
    body: msg,
    basicProperties: properties);
#endregion

#region Response

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(
    queue: replyQueueName,
    autoAck: true,
    consumer);

consumer.Received += (sender, e) =>
{
    if (e.BasicProperties.CorrelationId == correlationId)
        Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

#endregion


Console.Read();
