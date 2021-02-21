using btg_pqr_back.Core.Entities;
using btg_pqr_back.Core.Interfaces.Repository;
using btg_pqr_back.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace btg_pqr_back.Infrastructure.Repositories
{
    class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _entity;

        public Repository(AppDbContext context)
        {
            _entity = context.Set<T>();
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            var list = _entity.ToList();

            foreach (var item in list)
            {
                yield return item;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _entity.FindAsync(id);
            return entity;
        }

        public async Task<int> AddAsync(T entity)
        {
            await _entity.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(IEnumerable<T> entities)
        {
            await _entity.AddRangeAsync(entities);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _entity.Attach(entity);
            _entity.Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<T> entities)
        {
            _entity.AttachRange(entities);
            _entity.UpdateRange(entities);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await _entity.FindAsync(id);
            _entity.Attach(entity);
            _entity.Remove(entity);
            return await _context.SaveChangesAsync();
        }
    }
}