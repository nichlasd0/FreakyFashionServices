using System;
using System.Collections.Generic;
using System.Text;

namespace FreakyFashionServices.OrderProcessor.Models.DTO
{
    class CreateOrderDto
    {
        public string CustomerIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<ItemsDto> ItemsDto { get; set; } = new List<ItemsDto>();
    }
}
