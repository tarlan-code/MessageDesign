using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Settings;
using System.Text;

ChannelCreator channelCreator = new();

IModel channel = channelCreator.channel;

string exchangeName = "Publish/Subscribe";

channel.ExchangeDeclare(exchange: exchangeName,
    type: ExchangeType.Fanout,
    durable: false,
    autoDelete: true);

string queueName = channel.QueueDeclare().QueueName;


channel.QueueBind(queue: queueName,
    exchange: exchangeName,
    routingKey: String.Empty);

channel.BasicQos(prefetchSize: 0, //Serverin goture bileceyi mesaj sayi 0=Sonsuz sayda
    prefetchCount: 1, //Serverin eyni anda ishleye bileceyi mesaj sayi 0=Sonsuz sayda
    global: true); //true: Baglantida olan butun consumer-lar bele davranacaq, false: kanaldaki butun consumerlar bele davranacaq

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(queue:queueName,
    autoAck: true,
    consumer: consumer);

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();



