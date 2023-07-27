using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Runtime.CompilerServices;
using System.Text;

namespace Consumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new()
            {
                HostName = "localhost"
            };

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "my_queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += OnReceiveMessage;

            channel.BasicConsume(
                queue: "my_queue",
                autoAck: true,
                consumer: consumer);

            Console.ReadKey();
        }

        public static void OnReceiveMessage(object? sender, BasicDeliverEventArgs ea)
        {
            byte[] body = ea.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Recebida: {message}");
        }
    }
}