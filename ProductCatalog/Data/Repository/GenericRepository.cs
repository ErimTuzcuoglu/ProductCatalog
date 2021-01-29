using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ProductCatalog.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private DbSet<T> table;

        public GenericRepository(DbContext context)
        {
            this._context = context;
            table = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await table.FindAsync(id);
        }

        public T Add(T obj)
        {
            table.Add(obj);
            return obj;
        }

        public T Update(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            table.Update(obj);
            return obj;
        }

        public T Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
            return existing;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}