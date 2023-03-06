using AspNetCoreFirstExample.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreFirstExample.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class OrnekController : Controller
    {
        public class Product
        {
            public int Id { get; set; }
            public string? Name { get; set; }
        }

        [CustomResultFilter("x-version","1.0")]
        public IActionResult Index()
        {
            //ViewBag.name = "ASP.Net Core";

            //ViewData["age"] = 30;

            //ViewData["names"] = new List<string>() { "ahmet", "mehmet", "hasan" };

            //ViewBag.person = new { Id = 1, Name = "ahmet", Age = 23 };

            //@ViewBag.name = "ahmet";

            //TempData["surname"] = "Yıldız";

            var productList = new List<Product>()
            {
                    new() {Id = 1, Name = "Kalem"},
                    new() {Id = 2, Name = "Defter"},
                    new() {Id = 3, Name = "Silgi"}

            };

            return View(productList);
        }

        public IActionResult Index2()
        {
            return View();
        }

        public IActionResult Index3()
        {
            return RedirectToAction("Index", "Ornek");
        }

        public IActionResult ParametreView(int id)
        {

            return RedirectToAction("JsonResultParametre", "Ornek", new { id = id });
        }

        public IActionResult ContentResult()
        {
            return Content("ContentResult");
        }

        public IActionResult JsonResult()
        {
            return Json(new { Id = 1, name = "kalem", price = 15 });

        }
        public IActionResult JsonResultParametre(int id)
        {
            return Json(new { Id = id });

        }

        public IActionResult EmptyrResult()
        {
            return new EmptyResult();
        }

    }
}
