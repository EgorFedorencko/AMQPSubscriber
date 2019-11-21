using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Configuration;
using System.Text;
using System.Threading;

namespace ConsoleApp42
{
    class Program
    {
        static void Main(string[] args)
        {

            InquiryRepository repository = new InquiryRepository();

            var factory = new ConnectionFactory { HostName = "myrabbitmqi7jkr3l3hvqoi-vm0.westeurope.cloudapp.azure.com", UserName = "user", Password = "6iK=Bb!9#dyv2.P" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Inquiry", durable: false, exclusive: false, autoDelete: false, arguments: null);
                channel.BasicQos(0, 1, false);
                var consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(queue: "Inquiry", autoAck: false, consumer: consumer);
                consumer.Received += (model, ea) =>
                {
                    Inquiry inquiry = JsonConvert.DeserializeObject<Inquiry>(Encoding.UTF8.GetString(ea.Body));
                    repository.AddInquiry(inquiry);
                    channel.BasicAck(ea.DeliveryTag, false);

                };

            }

            Console.ReadLine();

        }
    }
}
