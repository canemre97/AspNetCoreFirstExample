using AspNetCoreFirstExample.Web.Models;
using AspNetCoreFirstExample.Web.ViewModels;
using AutoMapper;

namespace AspNetCoreFirstExample.Web.Mapping
{
    public class ViewModelMapping:Profile
    {
        public ViewModelMapping()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Visitor, VisitorViewModel>().ReverseMap();
        }
    }
}
