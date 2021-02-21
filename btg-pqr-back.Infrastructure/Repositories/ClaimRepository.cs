using btg_pqr_back.Core.Entities;
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
    public class ClaimRepository : IClaimRepository<ClaimEntity>
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<ClaimEntity> _entity;

        public ClaimRepository(AppDbContext context)
        {
            _entity = context.Set<ClaimEntity>();
            _context = context;
        }

        public IEnumerable<ClaimEntity> GetAll()
        {
            var list = _entity.ToList();

            foreach (var item in list)
            {
                yield return item;
            }
        }

        public IEnumerable<ClaimEntity> GetAllByClaimId(int claimId)
        {
            var list = _entity.Where(x => x.ClaimId == claimId);

            foreach (var item in list)
            {
                yield return item;
            }
        }

        public IEnumerable<ClaimEntity> GetAllByPqrId(int pqrId)
        {
            var list = _entity.Where(x => x.PqrId == pqrId);

            foreach (var item in list)
            {
                yield return item;
            }
        }

        public async Task<ClaimEntity> GetByIdAsync(int id)
        {
            var entity = await _entity.FindAsync(id);
            return entity;
        }

        public async Task<int> AddAsync(ClaimEntity entity)
        {
            await _entity.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(IEnumerable<ClaimEntity> entities)
        {
            await _entity.AddRangeAsync(entities);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(ClaimEntity entity)
        {
            _entity.Attach(entity);
            _entity.Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<ClaimEntity> entities)
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
