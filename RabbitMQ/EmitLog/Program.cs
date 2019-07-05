using RabbitMQ.Client;
using System;
using System.Text;

namespace EmitLog
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Emit Log!");

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // 定义一个交换器
                    channel.ExchangeDeclare(exchange: "logs", type: "fanout");

                    var message = GetMessage(args);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "logs",
                                        routingKey: "",
                                        basicProperties: null,
                                        body: body);

                    Console.WriteLine(" [X] Sent {0}", message);
                }
            }

            Console.WriteLine(" Press [enter] to exit");
            Console.ReadLine();
        }


        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }

    }
}
