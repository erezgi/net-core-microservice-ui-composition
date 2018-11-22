using Common;
using Common.Data;
using Consumer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.Controllers
{
    public class StarViewComponent : ViewComponent
    {
        private Services _services { get; set; }

        public StarViewComponent(Services services)
        {
            _services = services;
        }

        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {
            var star = await ServiceData.GetAsync<Star>(new System.Uri($"{_services.StarServiceUrl}/api/stars/{productId}")); ;
            return View(star);
        }
    }
}
