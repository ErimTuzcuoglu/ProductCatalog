using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Entities;

namespace ProductCatalog.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<Product> Products { get; set; }
        public Task<int> SaveChangesAsync();
    }
}