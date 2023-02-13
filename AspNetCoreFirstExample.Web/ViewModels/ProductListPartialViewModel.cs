using NuGet.Protocol.Core.Types;

namespace AspNetCoreFirstExample.Web.ViewModels
{
    public class ProductListPartialViewModel
    {
        public List<ProductPartialViewModel> Products { get; set; }
    }

    public class ProductPartialViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price{ get; set; }
        public int Stock { get; set; }
    }
}
