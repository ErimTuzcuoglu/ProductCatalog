using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProductCatalog.Common;
using ProductCatalog.Entities;
using ProductCatalog.Models;

namespace ProductCatalog.Service.Contract
{
    public interface IProductService
    {
        public Task<Response<List<ProductGetModel>>> SearchProductByName(string name);
        public Task<Response<ProductGetModel>> GetProduct(long id);
        public Task<Response<List<ProductGetModel>>> GetProducts();
        public Task<IEnumerable<Product>> GetProductsForExcelExport();
        public Task<Response<bool>> AddProduct(ProductPostModel model);
        public Task<Response<bool>> UpdateProduct(ProductUpdateModel model);
        public Task<Response<bool>> DeleteProduct(long id);
        public string PreparePhoto(IFormFile photo);
    }
}