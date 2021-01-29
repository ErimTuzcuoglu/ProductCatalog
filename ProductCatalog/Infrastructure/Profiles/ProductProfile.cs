using AutoMapper;
using ProductCatalog.Entities;
using ProductCatalog.Models;

namespace ProductCatalog.Infrastructure.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductPostModel, Product>();
            CreateMap<ProductUpdateModel, Product>().ForMember(p => p.Photo, opt => opt.Ignore());
            CreateMap<Product, ProductGetModel>();
        }
    }
}