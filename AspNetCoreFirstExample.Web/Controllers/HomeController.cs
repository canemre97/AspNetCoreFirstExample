using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AspNetCoreFirstExample.Web.Helpers;
using AspNetCoreFirstExample.Web.Models;
using AspNetCoreFirstExample.Web.ViewModels;
using AutoMapper;

namespace AspNetCoreFirstExample.Web.Controllers
{
    public class HomeController : Controller
    {
        //Helper _helper;

        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(AppDbContext context, ILogger<HomeController> logger /* Helper helper*/, IMapper mapper)
        {
            _logger = logger;
            // _helper = helper;
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = _context.Products.OrderByDescending(x => x.Id).Select(x => new ProductPartialViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock

            }).ToList();

            ViewBag.productListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
            };

            return View();
        }

        public IActionResult Privacy()
        {
            //var text = "asp.net";
            //var upperText = _helper.Upper(text);

            var products = _context.Products.OrderByDescending(x => x.Id).Select(x => new ProductPartialViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock

            }).ToList();

            ViewBag.productListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
            };

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Visitor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveVisitorComment(VisitorViewModel visitorViewModel)
        {
            try
            {
                var visitor = _mapper.Map<Visitor>(visitorViewModel);
                visitor.Created = DateTime.Now;

                _context.Visitors.Add(visitor);
                _context.SaveChanges();

                TempData["result"] = "Yorum kaydedilmiştir.";

                return RedirectToAction(nameof(HomeController.Visitor));
            }
            catch (Exception e)
            {
                TempData["result"] = "Yorum kaydedilirken bir hata meydana geldi.";

                return RedirectToAction(nameof(HomeController.Visitor));
            }
        }
    }
}