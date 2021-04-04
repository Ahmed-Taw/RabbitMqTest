using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Receive
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            Console.WriteLine(" Press [enter] to Listen to firstQueue.");
            Console.ReadLine();

            using (var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "firstQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += Consumer_Received;

                // after we created the consumer we should consume from the queue using it 

                channel.BasicConsume("firstQueue", true, consumer);

                
            }
            Console.WriteLine(" Press [enter] to Listen to SecondQueue.");
            Console.ReadLine();


            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "secondQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += Consumer_Received;

                // after we created the consumer we should consume from the queue using it 

                channel.BasicConsume("secondQueue", true, consumer);


            }

            Console.ReadLine();
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var messageString = Encoding.UTF8.GetString(body.ToArray());

            Console.WriteLine($"[x] Receivied Message:  {messageString}");
        }
    }
}
