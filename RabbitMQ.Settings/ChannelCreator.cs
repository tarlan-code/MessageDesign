using RabbitMQ.Client;

namespace RabbitMQ.Settings;

public class ChannelCreator
{
    public IModel channel { get; set; }

    public ChannelCreator()
    {
        ConnectionFactory factory = new() { HostName = "localhost" };

        IConnection connection = factory.CreateConnection();

        channel = connection.CreateModel();
    }
}
