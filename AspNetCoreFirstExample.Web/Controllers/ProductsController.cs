using AspNetCoreFirstExample.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreFirstExample.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Evaluation;
using AspNetCoreFirstExample.Web.ViewModels;
using AspNetCoreFirstExample.Web.Filters;
using Microsoft.Extensions.FileProviders;

namespace AspNetCoreFirstExample.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileProvider _fileProvider;

        //private IHelper _helper;

        private readonly IMapper _mapper;

        public ProductRepository Repository { get; }

        public ProductsController(AppDbContext context, IHelper helper, IMapper mapper, IFileProvider fileProvider)
        {
            //DI Container
            //Dependency Injection Pattern
            Repository = new ProductRepository();

            _context = context;
            //_helper = helper;
            _mapper = mapper;
            _fileProvider = fileProvider;

            //if (!_context.Products.Any())
            //{
            //    _context.Products.Add(new Product() { Name = "Kalem1", Price = 100, Stock = 100, Color = "Blue" });
            //    _context.Products.Add(new Product() { Name = "Kalem2", Price = 200, Stock = 100, Color = "Blue" });
            //    _context.Products.Add(new Product() { Name = "Kalem3", Price = 300, Stock = 100, Color = "Blue" });
            //    _context.Products.Add(new Product() { Name = "Kalem4", Price = 400, Stock = 100, Color = "Blue" });

            //    _context.SaveChanges();
            //}
        }

        //[CacheResourceFilter]
        public IActionResult Index( /*[FromServices] IHelper helper2*/)
        {
            //var text = "Asp.Net";
            //var upperText = _helper.Upper(text);

            //var status = _helper.Equals(helper2);


            var products = _context.Products.ToList();

            //return View(products);

            return View(_mapper.Map<List<ProductViewModel>>(products));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        //[Route("[controller]/[action]/{productId}")]
        [Route("urunler/urun/{productId}", Name = "product")]
        public IActionResult GetById(int productId)
        {
            var product = _context.Products.Find(productId);

            return View(_mapper.Map<ProductViewModel>(product));
        }

        //[HttpGet("{page}/{pagesize}")]
        [Route("[controller]/[action]/{page}/{pagesize}", Name = "productpage")]
        public IActionResult Pages(int page, int pageSize)
        {
            var products = _context.Products.Skip((page - 1) * pageSize).Take(pageSize).ToList();


            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            return View(_mapper.Map<List<ProductViewModel>>(products));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}")]
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
                {"1 Ay", 1},
                {"3 Ay", 3},
                {"6 Ay", 6},
                {"12 Ay", 12}
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new() {Data = "Mavi", Value = "Mavi"},
                new() {Data = "Sarı", Value = "Sarı"},
                new() {Data = "Kırmızı", Value = "Kırmızı"},
                new() {Data = "Siyah", Value = "Siyah"}
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


            //if (!string.IsNullOrEmpty(newProduct.Name) && newProduct.Name.StartsWith("A"))
            //{
            //    ModelState.AddModelError(String.Empty, "Ürün ismi A harfi ile başlayamaz!");
            //}

            //ViewBag.Expire = new Dictionary<string, int>()
            //{
            //    {"1 Ay", 1},
            //    {"3 Ay", 3},
            //    {"6 Ay", 6},
            //    {"12 Ay", 12}
            //};

            //ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            //{
            //    new() {Data = "Mavi", Value = "Mavi"},
            //    new() {Data = "Sarı", Value = "Sarı"},
            //    new() {Data = "Kırmızı", Value = "Kırmızı"},
            //    new() {Data = "Siyah", Value = "Siyah"}
            //}, "Value", "Data");


            IActionResult result = null;


            if (ModelState.IsValid)
            {
                try
                {
                    var product = _mapper.Map<Product>(newProduct);

                    if (newProduct.Image != null && newProduct.Image.Length > 0)
                    {
                        var root = _fileProvider.GetDirectoryContents("wwwroot");

                        var images = root.First(x => x.Name == "images");

                        var randomImageName = Guid.NewGuid() + Path.GetExtension(newProduct.Image.FileName);

                        var path = Path.Combine(images.PhysicalPath, randomImageName);

                        using var stream = new FileStream(path, FileMode.Create);

                        newProduct.Image.CopyTo(stream);
              
                        product.ImagePath = randomImageName;
                    }


                    //throw new Exception("DB Hatası");
                    _context.Products.Add(product);
                    _context.SaveChanges();

                    TempData["status"] = "Ürün başarıyla eklendi.";
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, "Ürün kaydedilirken bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz!");

                    result = View();
                }
            }
            else
            {
                result = View();
            }


            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay", 1},
                {"3 Ay", 3},
                {"6 Ay", 6},
                {"12 Ay", 12}
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new() {Data = "Mavi", Value = "Mavi"},
                new() {Data = "Sarı", Value = "Sarı"},
                new() {Data = "Kırmızı", Value = "Kırmızı"},
                new() {Data = "Siyah", Value = "Siyah"}
            }, "Value", "Data");

            return result;
        }

        [HttpGet]
        public IActionResult SaveProduct(Product newProduct)
        {
            _context.Products.Add(newProduct);
            _context.SaveChanges();

            return View();
        }

        [ServiceFilter(typeof(NotFoundFilter))]
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

            return View(_mapper.Map<ProductUpdateViewModel>(product));
        }

        [HttpPost]
        public IActionResult Update(ProductUpdateViewModel updateProduct /*,int productId*/)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ExpireValue = updateProduct.Expire;
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
                }, "Value", "Data", updateProduct.Color);

                return View();
            }


            if (updateProduct.Image != null && updateProduct.Image.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");

                var images = root.First(x => x.Name == "images");

                var randomImageName = Guid.NewGuid() + Path.GetExtension(updateProduct.Image.FileName);

                var path = Path.Combine(images.PhysicalPath, randomImageName);

                using var stream = new FileStream(path, FileMode.Create);

                updateProduct.Image.CopyTo(stream);

                updateProduct.ImagePath = randomImageName;
            }


            _context.Products.Update(_mapper.Map<Product>(updateProduct));
            _context.SaveChanges();
            TempData["status"] = "Ürün başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult HasProductName(string Name)
        {
            var anyProduct = _context.Products.Any(x => x.Name.ToLower() == Name.ToLower());
            if (anyProduct)
            {
                return Json("Ürün ismi veri tabanında bulunmaktadır!");
            }
            else
            {
                return Json(true);
            }

        }
    }
}
