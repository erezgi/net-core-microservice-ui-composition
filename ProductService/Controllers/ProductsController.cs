using Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // GET api/products
        [HttpGet]
        public async Task<ActionResult<IList<Product>>> GetAsync()
        {
            var dataContent = await System.IO.File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "Database", "data.json"));
            var data = JsonConvert.DeserializeObject<IList<Product>>(dataContent);
            return data.ToList();
        }

        // GET api/products/5
        [HttpGet("{productId}")]
        public async Task<ActionResult<Product>> GetAsync(int productId)
        {
            if (productId <= 0)
            {
                return BadRequest();
            }

            var dataContent = await System.IO.File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "Database", "data.json"));
            var data = JsonConvert.DeserializeObject<IList<Product>>(dataContent);
            if (!data.Any(d => d.Id == productId))
            {
                return NoContent();
            }

            return data.FirstOrDefault(d => d.Id == productId);
        }

        // POST api/products
        [HttpPost]
        public async Task<ActionResult<Product>> PostAsync([FromBody] Product value)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Database", "data.json");
            var dataContent = await System.IO.File.ReadAllTextAsync(filePath);
            var data = JsonConvert.DeserializeObject<IList<Product>>(dataContent);
            data.Add(value);
            await System.IO.File.WriteAllTextAsync(filePath, JsonConvert.SerializeObject(data));
            return CreatedAtAction("GetAsync", value);
        }
    }
}
