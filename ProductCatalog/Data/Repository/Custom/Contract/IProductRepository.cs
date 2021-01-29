using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Entities;

namespace ProductCatalog.Data.Repository.Custom.Contract
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<Product> SearchByCode(string code);
        public Task<IEnumerable<Product>> SearchByName(string name);
    }
}