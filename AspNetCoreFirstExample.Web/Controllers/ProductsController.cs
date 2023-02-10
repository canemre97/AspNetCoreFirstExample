using AspNetCoreFirstExample.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreFirstExample.Web.Models;

namespace AspNetCoreFirstExample.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        private IHelper _helper;

        public ProductRepository Repository { get; }

        public ProductsController(AppDbContext context, IHelper helper)
        {
            //DI Container
            //Dependency Injection Pattern
            Repository = new ProductRepository();

            _context = context;
            _helper = helper;

            //if (!_context.Products.Any())
            //{
            //    _context.Products.Add(new Product() { Name = "Kalem1", Price = 100, Stock = 100, Color = "Blue" });
            //    _context.Products.Add(new Product() { Name = "Kalem2", Price = 200, Stock = 100, Color = "Blue" });
            //    _context.Products.Add(new Product() { Name = "Kalem3", Price = 300, Stock = 100, Color = "Blue" });
            //    _context.Products.Add(new Product() { Name = "Kalem4", Price = 400, Stock = 100, Color = "Blue" });

            //    _context.SaveChanges();
            //}
        }


        public IActionResult Index([FromServices]IHelper helper2)
        {
            var text = "Asp.Net";
            var upperText = _helper.Upper(text);

            var status = _helper.Equals(helper2);

            var products = _context.Products.ToList();

            return View(products);
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
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product newProduct)
        {
            //1.Yöntem
            //var name = HttpContext.Request.Form["Name"].ToString();
            //var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            //var stock = int.Parse(Request.Form["Stock"].ToString());
            //var color = Request.Form["Color"].ToString();

            //2. Yöntem
            //Product newProduct = new Product() {Name = Name, Price = Price, Stock = Stock, Color = Color};

            _context.Products.Add(newProduct);
            _context.SaveChanges();
            TempData["status"] = "Ürün başarıyla eklendi.";
            return RedirectToAction("Index");
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
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product updateProduct,int productId)
        {
            updateProduct.Id = productId;
            _context.Products.Update(updateProduct);
            _context.SaveChanges();
            TempData["status"] = "Ürün başarıyla güncellendi.";
            return RedirectToAction("Index");
        }
    }
}
