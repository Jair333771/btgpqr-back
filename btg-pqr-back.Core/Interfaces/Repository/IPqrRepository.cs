using btg_pqr_back.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace btg_pqr_back.Core.Interfaces.Repository
{
    public interface IPqrRepository<T> : IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAllByType(int type);
        IEnumerable<T> GetAllBUserName(string username);
        Task<PqrEntity> GetByUserAndActive(string userName);
        IEnumerable<T> GetAllPetitionsAndComplaintByUser(string userName);
        IEnumerable<T> GetClaimByPqrId(int pqrId);
    }
}
