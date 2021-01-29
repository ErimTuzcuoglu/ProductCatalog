using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ProductCatalog.Data;
using ProductCatalog.Data.Repository;
using ProductCatalog.Data.Repository.Custom;
using ProductCatalog.Data.Repository.Custom.Contract;
using ProductCatalog.Service;
using ProductCatalog.Service.Contract;

namespace ProductCatalog.Infrastructure
{
    public static class ConfigureServiceContainer
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ProductCatalog", Version = "v1"});
            });

            services.AddAutoMapper(typeof(ConfigureServiceContainer));

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
        }
    }
}