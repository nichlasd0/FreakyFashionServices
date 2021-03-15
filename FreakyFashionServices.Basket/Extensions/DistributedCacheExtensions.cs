using FreakyFashionServices.Basket.Models.DTO;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreakyFashionServices.Basket.Extensions
{
    public static class DistributedCacheExtensions
    {

        public static async Task SetBasketAsync<T>(this IDistributedCache cache,
            string id,
            T data,
            TimeSpan? asboluteExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();

            options.AbsoluteExpirationRelativeToNow = asboluteExpireTime ?? TimeSpan.FromMinutes(10);

            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(id, jsonData, options);
        }

        public static async Task<T> GetBasketAsync<T>(this IDistributedCache cache, string id)
        {
            var jsonData = await cache.GetStringAsync(id);

            if (jsonData is null)
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
