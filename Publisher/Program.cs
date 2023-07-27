using RabbitMQ.Client;
using System.Text;

namespace Publisher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory()
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

            string message = "Teste envio de mensagem";

            byte[] body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: "my_queue",
                basicProperties: null,
                body: body);

            Console.WriteLine($" [x] Enviada: {message}");

            Console.ReadKey();
        }
    }
}