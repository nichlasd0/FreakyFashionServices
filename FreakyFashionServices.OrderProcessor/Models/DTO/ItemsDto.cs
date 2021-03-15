using System;
using System.Collections.Generic;
using System.Text;

namespace FreakyFashionServices.OrderProcessor.Models.DTO
{
    class ItemsDto
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
