using System.Collections.Generic;
using System.Threading.Tasks;

namespace btg_pqr_back.Core.Interfaces.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        Task<int> AddAsync(T entity);
        Task<int> AddRangeAsync(IEnumerable<T> entities);
        Task<int> UpdateAsync(T entity);
        Task<int> UpdateRangeAsync(IEnumerable<T> entities);
        Task<int> DeleteAsync(int id);
    }
}
