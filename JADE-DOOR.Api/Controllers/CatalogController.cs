using Microsoft.AspNetCore.Mvc;
using JADE_DOOR.Domain.Catalog;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;   
using JADE_DOOR.Data;
using Microsoft.AspNetCore.Authorization; 

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
        public ActionResult GetItem(int id)
        {
            var item = _db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPost]
        public ActionResult Post(Item item)
        {
            _db.Items.Add(item);
            _db.SaveChanges();
            return Created($"/catalog/{item.Id}", item);
        }
        [HttpPost("{id:int}/ratings")]
        public IActionResult PostRating(int id, [FromBody] Rating rating)
        {
            var item = _db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            // Use domain method to add rating so any domain invariants/validation are applied
            item.AddRating(rating);
            _db.SaveChanges();

            return Ok(item);
        }
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Item item)
        {
            // Validate route id matches body id
            if (id != item.Id)
            {
                return BadRequest();
            }

            // Ensure the item exists
            var existing = _db.Items.Find(id);
            if (existing == null)
            {
                return NotFound();
            }

            // Mark the incoming entity as modified and persist
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return NoContent();
        }
        [HttpDelete("{id:int}")]
        [Authorize("delete:catalog")]
        public IActionResult Delete(int id)
        {
            var item = _db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            _db.Items.Remove(item);
            _db.SaveChanges();

            return Ok(item);
        }


    }

}
