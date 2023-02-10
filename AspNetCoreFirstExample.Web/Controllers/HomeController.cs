using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AspNetCoreFirstExample.Web.Helpers;
using AspNetCoreFirstExample.Web.Models;

namespace AspNetCoreFirstExample.Web.Controllers
{
    public class HomeController : Controller
    {
        //Helper _helper;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger /* Helper helper*/)
        {
            _logger = logger;
           // _helper = helper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            //var text = "asp.net";
            //var upperText = _helper.Upper(text);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}