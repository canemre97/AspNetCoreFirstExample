using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreFirstExample.Web.Controllers
{
    public class CookieController : Controller
    {
        public IActionResult CookieCreate()
        {
            HttpContext.Response.Cookies.Append("background-color", "red", new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(2)
            });

            return View();
        }

        public IActionResult CookieRead()
        {
            var bgcolor = HttpContext.Request.Cookies["background-color"];

            ViewBag.bgcolor = bgcolor;

            return View();

        }
    }
}
