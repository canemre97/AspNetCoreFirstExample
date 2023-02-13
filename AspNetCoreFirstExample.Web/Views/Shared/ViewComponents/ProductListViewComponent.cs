using Microsoft.AspNetCore.Mvc;
using AspNetCoreFirstExample.Web.Models;
using AspNetCoreFirstExample.Web.ViewModels;

namespace AspNetCoreFirstExample.Web.Views.Shared.ViewComponents
{
    public class ProductListViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly AppDbContext _context;

        public ProductListViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int type = 1)
        {
            var viewModels = _context.Products.Select(x => new ProductListComponentViewModel()
            {
                Name = x.Name,
                Description = x.Description,
            }).ToList();

            if (type == 1)
            {
                return View("Default", viewModels);
            }
            else
            {
                return View("Type2", viewModels);
            }

        }
    }
}
