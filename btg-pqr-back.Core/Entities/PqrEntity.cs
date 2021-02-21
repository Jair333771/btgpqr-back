using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace btg_pqr_back.Core.Entities
{
    [Table("pqrs")]
    public class PqrEntity : BaseEntity
    {
        public string MessageUser { get; set; }
        public string ResponseAdmin { get; set; }
        public DateTime DateRequest { get; set; }
        public DateTime? DateResponse { get; set; }
        public string UserName { get; set; }
        public int Type { get; set; }
        public bool Active { get; set; }

        public double CountDays() => DateTime.Now.Subtract(DateRequest).TotalDays;

        public bool CanClaim()
        {
            var flag = false;
            var days = CountDays();
            if ((string.IsNullOrEmpty(ResponseAdmin) && DateResponse != null) || days > 5)
            {
                flag = true;
            }
            return flag;
        }

        public bool IsActive()
        {
            return DateResponse != null;
        }
    }
}
