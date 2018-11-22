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
    public class CategoryViewComponent : ViewComponent
    {
        private Services _services { get; set; }

        public CategoryViewComponent(Services services)
        {
            _services = services;
        }

        public async Task<IViewComponentResult> InvokeAsync(int categoryId)
        {
            var category = await ServiceData.GetAsync<Category>(new System.Uri($"{_services.CategoryServiceUrl}/api/categories/{categoryId}")); ;
            return View(category);
        }
    }
}
