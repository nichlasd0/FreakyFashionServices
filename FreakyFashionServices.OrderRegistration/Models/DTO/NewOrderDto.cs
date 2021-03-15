using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreakyFashionServices.OrderRegistration.Models.DTO
{
    public class NewOrderDto
    {
        public string CustomerIdentifier { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
