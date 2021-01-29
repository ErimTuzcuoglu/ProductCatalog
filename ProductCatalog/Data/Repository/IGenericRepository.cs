using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ProductCatalog.Data.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(object id);
        T Add(T obj);
        T Update(T obj);
        T Delete(object id);
        Task SaveAsync();
        void Dispose();
        
    }
}