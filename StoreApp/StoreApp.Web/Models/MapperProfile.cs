using AutoMapper;
using StoreApp.Data.Concrete;

namespace StoreApp.Web.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>();
            CreateMap<Product, ProductListViewModel>();
            CreateMap<ProductListViewModel, Product>();
        }
    }

}