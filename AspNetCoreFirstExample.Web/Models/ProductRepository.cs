using System.Data;

namespace AspNetCoreFirstExample.Web.Models
{
    public class ProductRepository
    {
        private static List<Product> _products = new List<Product>()
        {
        //new() { Id = 1, Name = "Kalem", Price = 100, Stock = 200 },
        //new () { Id = 2, Name = "Kalem2", Price = 200, Stock = 200 },
        //new () { Id = 3, Name = "Kalem3", Price = 300, Stock = 200 },
        //new() { Id = 4, Name = "Kalem4", Price = 400, Stock = 200 }
        };


        public List<Product> GetAll() => _products;

        public void Add(Product product) => _products.Add(product);

        public void Remove(int id)
        {
            var hasProduct = _products.FirstOrDefault(x => x.Id == id);
            if (hasProduct is null)
            {
                throw new Exception($"Bu id({id})'ye sahip ürün bulunmamaktadır");
            }

            _products.Remove(hasProduct);
        }

        public void Update(Product updateProduct)
        {
            var hasProduct = _products.FirstOrDefault(x => x.Id == updateProduct.Id);
            if (hasProduct is null)
            {
                throw new Exception($"Bu id({updateProduct.Id})'ye sahip ürün bulunmamaktadır");
            }

            hasProduct.Name = updateProduct.Name;
            hasProduct.Price = updateProduct.Price;
            hasProduct.Stock = updateProduct.Stock;

            var index = _products.FindIndex(x => x.Id == updateProduct.Id);

            _products[index] = updateProduct;
        }
    }
}
