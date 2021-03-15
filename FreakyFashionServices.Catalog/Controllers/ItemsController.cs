using System.Collections;
using FreakyFashionServices.Catalog.Data;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using FreakyFashionServices.Catalog.Models.Domain;
using FreakyFashionServices.Catalog.Models.DTO;

namespace FreakyFashionServices.Catalog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ItemsDto> GetItems()
        {
            var items = _context.Items.ToList();
            var dto = items.Select(x => new ItemsDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                AvailableStock = x.AvailableStock
            });
            
            return dto;
        }

           [HttpGet("{id}")]
           public ActionResult<ItemsDto> GetItemsById(int id)
           {
               var foundItem = _context.Items.FirstOrDefault(x => x.Id == id);
               if (foundItem is null)
               {
                   return NotFound();
               }

               var dto = new ItemsDto
               {
                   Id = foundItem.Id,
                   Name = foundItem.Name,
                   Description = foundItem.Description,
                   Price = foundItem.Price,
                   AvailableStock = foundItem.AvailableStock

               };
               return dto;
           }
    }
}