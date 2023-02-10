using AspNetCoreFirstExample.Web.Models;
using AspNetCoreFirstExample.Web.Models.ViewModels;
using AutoMapper;

namespace AspNetCoreFirstExample.Web.Mapping
{
    public class ViewModelMapping:Profile
    {
        public ViewModelMapping()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
