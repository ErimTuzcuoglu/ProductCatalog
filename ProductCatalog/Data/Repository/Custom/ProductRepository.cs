using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.Repository.Custom.Contract;
using ProductCatalog.Entities;

namespace ProductCatalog.Data.Repository.Custom
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> SearchByCode(string code)
        {
            return _context.Set<Product>().Where(p => p.Code.Equals(code)).FirstOrDefault();
        }

        public async Task<IEnumerable<Product>> SearchByName(string name)
        {
            return _context.Set<Product>().Where(p => p.Name.Contains(name));
        }
    }
}