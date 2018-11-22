using Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StarService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarsController : ControllerBase
    {
        // GET api/healthy
        [HttpGet]
        public ActionResult<Star> Get()
        {
            return NoContent();
        }

        // GET api/stars/{productId}
        [HttpGet("{productId}")]
        public async Task<ActionResult<Star>> GetAsync(int productId)
        {
            if (productId <= 0)
            {
                return BadRequest();
            }

            var dataContent = await System.IO.File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "Database", "data.json"));
            var data = JsonConvert.DeserializeObject<IList<Star>>(dataContent);
            if (!data.Any(d => d.ProductId == productId))
            {
                return NoContent();
            }

            return data.FirstOrDefault(d => d.ProductId == productId);
        }

        // POST api/stars
        [HttpPost]
        public async Task<ActionResult<Star>> PostAsync([FromBody] Star value)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Database", "data.json");
            var dataContent = await System.IO.File.ReadAllTextAsync(filePath);
            var data = JsonConvert.DeserializeObject<IList<Star>>(dataContent);
            data.Add(value);
            await System.IO.File.WriteAllTextAsync(filePath, JsonConvert.SerializeObject(data));
            return CreatedAtAction("Get", null);
        }
    }
}
