using FreakyFashionServices.ProductPrice.Models.Domain;
using FreakyFashionServices.ProductPrice.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreakyFashionServices.ProductPrice.Controllers
{
    [ApiController]
    [Route("api/prices")]
    public class ProductPriceControllers : ControllerBase
    {

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetProducts(string product)
        {
            var articleNumbers = product.Split(',');
            List<ProductDto> productPrices = new List<ProductDto>();
            foreach (string articleNumber in articleNumbers)
            {
                productPrices.Add(new ProductDto(articleNumber));
            }

            return Ok(productPrices);
        }
    }
}
