using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreakyFashionServices.OrderProcessor.Models.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Items> Items { get; set; } = new List<Items>();

    }
}
