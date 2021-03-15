using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreakyFashionServices.Gateway.Controllers
{
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private readonly IHttpClientFactory clientFactory;

        public GatewayController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }


        [HttpGet]
        [Route("api/products")]
        public async Task<ActionResult<List<ProductDto>>> GetItems()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://freakyfashionservices.catalog/api/items/");

            request.Headers.Add("Accept", "application/json");

            var client = clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            var serializedItem = await response.Content.ReadAsStringAsync();
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var itemDto = JsonSerializer.Deserialize<List<ProductDto>>(serializedItem, serializeOptions);
            string productArticleString = "";
             foreach(var item in itemDto)
            {
                productArticleString += item.Id.ToString() + ",";
            }
            request = new HttpRequestMessage(HttpMethod.Get, "http://freakyfashionservices.productprice/api/prices?product="+productArticleString);
            request.Headers.Add("Accept", "application/json");
            response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {

                var serializedPrice = await response.Content.ReadAsStringAsync();
                var productPrice = JsonSerializer.Deserialize<List<ProductPrice>>(serializedPrice, serializeOptions);
                List<ProductDto> itemWithUpdatePrice = new List<ProductDto>();

                foreach (var item in itemDto)
                {
                    foreach (var article in productPrice)
                    {
                        if (article.ArticleNumber == item.Id.ToString())
                        {
                            ProductDto tempProduct = new ProductDto
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Description = item.Description,
                                Price = article.Price,
                                AvailableStock = item.AvailableStock
                            };
                            itemWithUpdatePrice.Add(tempProduct);
                        }
                    }
                }

                return Ok(itemWithUpdatePrice);
            }
            return NotFound();

        }


        public class ProductDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int Price { get; set; }
            public int AvailableStock { get; set; }
        }

        public class ProductPrice
        {
            public string ArticleNumber { get; set; }
            public int Price { get; set; }

        }
    }
}
