using btg_pqr_back.Core.Entities;
using System.Collections.Generic;

namespace btg_pqr_back.Core.Interfaces.Repository
{
    public interface IClaimRepository<T> : IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAllByClaimId(int claimId);
        IEnumerable<T> GetAllByPqrId(int pqrId);
    }
}
