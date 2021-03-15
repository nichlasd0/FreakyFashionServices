using System.Collections.Generic;

namespace FreakyFashionServices.OrderRegistration.Models.DTO
{
    public class CreateOrderDto
    {
        public string CustomerIdentifier { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public List<ItemsDto> Items { get; set; } = new List<ItemsDto>();
    }
}