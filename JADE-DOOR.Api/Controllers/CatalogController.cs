using Microsoft.AspNetCore.Mvc;
using JADE_DOOR.Domain.Catalog;
using System.Collections.Generic;   
using JADE_DOOR.Data;

namespace JADE_DOOR.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly StoreContext _db;

        public CatalogController(StoreContext db)
        {
            _db = db;
        }
        
        [HttpGet]
        public IActionResult GetItems()
        {
           
            return Ok(_db.Items);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetItem(int id)
        {
            var item = new Item("Shirt", "Ohio State shirt.", "Nike", 29.99m);
            item.Id = id;
            return Ok(item);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Item item)
        {
            if (item.Id == 0) item.Id = 42;
            return Created($"/catalog/{item.Id}", item);
        }
        [HttpPost("{id:int}/ratings")]
        public IActionResult PostRating(int id, [FromBody] Rating rating)
        {
            var item = new Item("Shirt", "Ohio State shirt.", "Nike", 29.99m)
            {
                Id = id
            };

            item.ns ??= new List<Rating>();
            item.Ratings.Add(rating);

            return Ok(item);
        }
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Item item)
        {
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }


    }

}
