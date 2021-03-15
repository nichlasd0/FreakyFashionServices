using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreakyFashionServices.Basket.Models.Domain
{
    internal class Basket
    {
        public string Id { get; set; }
        public IList<Items> Items { get; set; }
    }
}
