using Common;
using Common.Data;
using Consumer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consumer.Controllers
{
    public class HomeController : Controller
    {
        private Services _services { get; set; }

        public HomeController(Services services)
        {
            _services = services;
        }

        public async Task<IActionResult> IndexAsync()
        {
            IList<Product> products = await ServiceData.GetAsync<IList<Product>>(new System.Uri($"{_services.ProductServiceUrl}/api/products"));
            return View(products);
        }
    }
}
