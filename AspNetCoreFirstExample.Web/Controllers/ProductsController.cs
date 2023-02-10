using AspNetCoreFirstExample.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreFirstExample.Web.Models;
using AspNetCoreFirstExample.Web.Models.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Evaluation;

namespace AspNetCoreFirstExample.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        //private IHelper _helper;

        private readonly IMapper _mapper;

        public ProductRepository Repository { get; }

        public ProductsController(AppDbContext context, IHelper helper, IMapper mapper)
        {
            //DI Container
            //Dependency Injection Pattern
            Repository = new ProductRepository();

            _context = context;
            //_helper = helper;
            _mapper = mapper;

            //if (!_context.Products.Any())
            //{
            //    _context.Products.Add(new Product() { Name = "Kalem1", Price = 100, Stock = 100, Color = "Blue" });
            //    _context.Products.Add(new Product() { Name = "Kalem2", Price = 200, Stock = 100, Color = "Blue" });
            //    _context.Products.Add(new Product() { Name = "Kalem3", Price = 300, Stock = 100, Color = "Blue" });
            //    _context.Products.Add(new Product() { Name = "Kalem4", Price = 400, Stock = 100, Color = "Blue" });

            //    _context.SaveChanges();
            //}
        }


        public IActionResult Index(/*[FromServices] IHelper helper2*/)
        {
            //var text = "Asp.Net";
            //var upperText = _helper.Upper(text);

            //var status = _helper.Equals(helper2);


            var products = _context.Products.ToList();

            //return View(products);

            return View(_mapper.Map<List<ProductViewModel>>(products));
        }

        public IActionResult Remove(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product!);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay",1},
                {"3 Ay",3},
                {"6 Ay",6},
                {"12 Ay",12}
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new() {Data="Mavi",Value="Mavi"},
                new() {Data="Sarı",Value="Sarı"},
                new() {Data="Kırmızı",Value="Kırmızı"},
                new() {Data="Siyah",Value="Siyah"}
            }, "Value", "Data");

            //ViewBag.Expire = new List<string>() {"1 Ay", "3 Ay", "6 Ay", "12 Ay"};
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel newProduct)
        {
            //1.Yöntem
            //var name = HttpContext.Request.Form["Name"].ToString();
            //var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            //var stock = int.Parse(Request.Form["Stock"].ToString());
            //var color = Request.Form["Color"].ToString();

            //2. Yöntem
            //Product newProduct = new Product() {Name = Name, Price = Price, Stock = Stock, Color = Color};

            if (ModelState.IsValid)
            {
                _context.Products.Add(_mapper.Map<Product>(newProduct));
                _context.SaveChanges();

                TempData["status"] = "Ürün başarıyla eklendi.";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Expire = new Dictionary<string, int>()
                {
                    {"1 Ay",1},
                    {"3 Ay",3},
                    {"6 Ay",6},
                    {"12 Ay",12}
                };

                ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
                {
                    new() {Data="Mavi",Value="Mavi"},
                    new() {Data="Sarı",Value="Sarı"},
                    new() {Data="Kırmızı",Value="Kırmızı"},
                    new() {Data="Siyah",Value="Siyah"}
                }, "Value", "Data");

                return View();
            }

        }

        [HttpGet]
        public IActionResult SaveProduct(Product newProduct)
        {
            _context.Products.Add(newProduct);
            _context.SaveChanges();

            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);

            ViewBag.ExpireValue = product.Expire;
            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay",1},
                {"3 Ay",3},
                {"6 Ay",6},
                {"12 Ay",12}
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new() {Data="Mavi",Value="Mavi"},
                new() {Data="Sarı",Value="Sarı"},
                new() {Data="Kırmızı",Value="Kırmızı"},
                new() {Data="Siyah",Value="Siyah"}
            }, "Value", "Data", product.Color);

            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product updateProduct, int productId)
        {
            updateProduct.Id = productId;
            _context.Products.Update(updateProduct);
            _context.SaveChanges();
            TempData["status"] = "Ürün başarıyla güncellendi.";
            return RedirectToAction("Index");
        }
    }
}
