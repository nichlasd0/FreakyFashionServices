using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreakyFashionServices.ProductPrice.Models.DTO
{
    public class ProductDto
    {
        public string ArticleNumber { get; set; }
        public int Price { get; set; }
        public ProductDto(string articleNumber)
        {
            ArticleNumber = articleNumber;

            Random random = new Random();
            Price = random.Next(100, 1000);

        }
    }
}
