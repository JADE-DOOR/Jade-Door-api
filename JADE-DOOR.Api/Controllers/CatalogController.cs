using Microsoft.AspNetCore.Mvc;
using JADE_DOOR.Domain.Catalog;

namespace JADE_DOOR.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetItems()
        {
            return Ok("hello world.");
        }
    }
}