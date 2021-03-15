using FreakyFashionServices.OrderRegistration.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace FreakyFashionServices.OrderRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderRegistrationController : ControllerBase
    {
        private readonly IHttpClientFactory clientFactory;

        public OrderRegistrationController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }


        // POST api/<OrderRegistrationController>
        [HttpPost]
        public async Task<ActionResult> CreateOrder(NewOrderDto newOrderDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://freakyfashionservices.basket/api/basket/" + newOrderDto.CustomerIdentifier);

            request.Headers.Add("Accept", "application/json");

            var client = clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var serializedItem = await response.Content.ReadAsStringAsync();
                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var basketDto = System.Text.Json.JsonSerializer.Deserialize<Basket>(serializedItem, serializeOptions);

                CreateOrderDto order = new CreateOrderDto
                {
                    CustomerIdentifier = newOrderDto.CustomerIdentifier,
                    FirstName = newOrderDto.FirstName,
                    LastName = newOrderDto.LastName,

                };
                order.Items.AddRange(basketDto.Items);


                var factory = new ConnectionFactory
                {
                    Uri = new Uri("amqp://guest:guest@rabbit:5672")
                };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: "orders",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(order));

                channel.BasicPublish(exchange: "",
                                    routingKey: "orders",
                                    basicProperties: null,
                                    body: body);

                return Accepted();
            }
            return NotFound();
        }
        internal class Basket
        {
            public string Id { get; set; }
            public IList<ItemsDto> Items { get; set; }
        }
    }
}
