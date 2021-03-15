using System;
using System.Collections.Generic;
using System.Text;

namespace FreakyFashionServices.OrderProcessor.Models.Domain
{
    public class Items
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
