using btg_pqr_back.Core.Entities;
using btg_pqr_back.Core.Enums;
using btg_pqr_back.Core.Interfaces.Repository;
using btg_pqr_back.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace btg_pqr_back.Infrastructure.Repositories
{
    public class PqrRepository : IPqrRepository<PqrEntity>
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<PqrEntity> _entity;

        public PqrRepository(AppDbContext context)
        {
            _entity = context.Set<PqrEntity>();
            _context = context;
        }

        public IEnumerable<PqrEntity> GetAll()
        {
            var list = _entity.ToList();

            foreach (var item in list)
            {
                yield return item;
            }
        }

        public async Task<PqrEntity> GetByIdAsync(int id)
        {
            var entity = await _entity.FindAsync(id);
            return entity;
        }

        public async Task<int> AddAsync(PqrEntity entity)
        {
            entity.Active = true;
            entity.DateRequest = DateTime.Now;
            await _entity.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(IEnumerable<PqrEntity> entities)
        {
            await _entity.AddRangeAsync(entities);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(PqrEntity entity)
        {
            _entity.Attach(entity);
            _entity.Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<PqrEntity> entities)
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

        public IEnumerable<PqrEntity> GetAllByType(int type)
        {
            var list = _entity
                .Where(x => x.Type == type)
                .ToList();

            foreach (var item in list)
            {
                yield return item;
            }
        }

        public IEnumerable<PqrEntity> GetAllBUserName(string username)
        {
            var list = _entity
                .Where(x => x.UserName.Equals(username))
                .ToList();

            foreach (var item in list)
            {
                yield return item;
            }
        }

        public async Task<PqrEntity> GetByUserAndActive(string userName)
        {
            return await _entity
                .FirstOrDefaultAsync(x => 
                    x.UserName == userName &&
                    x.Active != false &&
                    x.Type == (int)PqrTypeEnum.Claim);
        }
    }
}