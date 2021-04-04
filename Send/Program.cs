using System;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "firstQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                string messageString = "FirstMessageTest";

                var messageBody = Encoding.UTF8.GetBytes(messageString);

                channel.BasicPublish(exchange: "", routingKey: "firstQueue", mandatory: false, basicProperties: null,body:messageBody);
                Console.WriteLine(" [x] Sent {0}", messageString);
            }

            Console.WriteLine(" Press [enter] to Send a Message in Second Queue.");
            var stringMessageFromtheUser = Console.ReadLine();

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "secondQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                string messageString = stringMessageFromtheUser;

                var messageBody = Encoding.UTF8.GetBytes(messageString);

                channel.BasicPublish(exchange: "", routingKey: "secondQueue", mandatory: false, basicProperties: null, body: messageBody);
                Console.WriteLine(" [x] Sent {0}", messageString);
            }

            Console.WriteLine(" Press [enter] Exit.");
             Console.ReadLine();
        }
    }

    //class Send
    //{
    //    public static void Main()
    //    {
    //        var factory = new ConnectionFactory() { HostName = "localhost" };

    //        using (var connection = factory.CreateConnection())
    //        using (var channel = connection.CreateModel())
    //        {
    //            channel.QueueDeclare(queue: "firstQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

    //            string messageString = "FirstMessageTest";

    //            var messageBody = Encoding.UTF8.GetBytes(messageString);

    //            channel.BasicPublish(exchange: "", routingKey: "firstQueue", mandatory: false, basicProperties: null);
    //            Console.WriteLine(" [x] Sent {0}", messageString);
    //        }

    //        Console.WriteLine(" Press [enter] to exit.");
    //        Console.ReadLine();
    //    }
    //}

}
