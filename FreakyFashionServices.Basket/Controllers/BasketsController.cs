using FreakyFashionServices.Basket.Models.DTO;
using FreakyFashionServices.Basket.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using FreakyFashionServices.Basket.Extensions;

namespace FreakyFashionServices.Basket.Controllers
{
    [ApiController]
    [Route("api/basket")]
    public class BasketsController : ControllerBase
    {
        private readonly IDistributedCache cache;
        public BasketsController(IDistributedCache cache)
        {
            this.cache = cache;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> CreateBasket(string id, BasketDto createBasket)
        {
            var options = new DistributedCacheEntryOptions();

            await cache.SetBasketAsync<BasketDto>(id, createBasket);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBasket(string id)
        {
            // var jsonData = await cache.GetStringAsync(getBasketDto.Id);
            var jsonData = await cache.GetBasketAsync<BasketDto>(id);

            if (jsonData is null)
            {
                return NotFound();
            }

            return Ok(jsonData);

        }
        
    }
}
