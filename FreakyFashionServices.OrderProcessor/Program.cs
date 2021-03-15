using FreakyFashionServices.OrderProcessor.Models.Domain;
using FreakyFashionServices.OrderProcessor.Models.DTO;
using FreakyFashionServices.OrderProcessor.Data;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FreakyFashionServices.OrderProcessor
{
    class Program
    {
        static readonly OrderContext context = new OrderContext();
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5010")
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "orders",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                var dto = JsonConvert.DeserializeObject<CreateOrderDto>(json);

                var order = new Order
                {
                    CustomerIdentifier = dto.CustomerIdentifier,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                };

                Console.WriteLine($"Processing orders...{order.CustomerIdentifier}");

                context.Database.Migrate();

                context.Orders.Add(order);
                context.SaveChanges();
            };

            channel.BasicConsume(queue: "orders",
                                autoAck: true,
                                consumer: consumer);

            Console.WriteLine("Press [enter] to exit");
            Console.ReadLine();
        }
    }
}
