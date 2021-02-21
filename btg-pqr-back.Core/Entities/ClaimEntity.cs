using System.ComponentModel.DataAnnotations.Schema;

namespace btg_pqr_back.Core.Entities
{
    [Table("claims")]
    public class ClaimEntity : BaseEntity
    {
        public int ClaimId { get; set; }
        public int PqrId { get; set; }
    }
}
