using Common;
using Common.Data;
using Consumer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Consumer.Controllers
{
    public class ProductController : Controller
    {
        private Services _services { get; set; }

        public ProductController(Services services)
        {
            _services = services;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateProductViewModel entity)
        {
            Task.Factory.ContinueWhenAll(
               new Task[] {

                   ServiceData.PostAsync<Product, Product>(new System.Uri($"{_services.ProductServiceUrl}/api/products"), entity.Product)
                        .ContinueWith((task) => {  _ = task.Result; }),
                     ServiceData.PostAsync<Star, Star>(new System.Uri($"{_services.StarServiceUrl}/api/stars"), entity.Star)
                        .ContinueWith((task) => {  _ = task.Result; }),
                   },
                   _ => { })
               .Wait();
            return CreatedAtAction("IndexAsync", "Home", null, null);
        }
    }
}
