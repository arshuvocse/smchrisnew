using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HealthCare_Dao
{
   public class ClaimDetailsDao
    {
        public int ClaimDetailsId { get; set; }

        public int? ReimbursFromMasterId { get; set; }

        public int? OIPDHeadOfExpenseId { get; set; }

        public DateTime? Dates { get; set; }

        public int? Numberofdays { get; set; }
        public int? ChildrenNo { get; set; }

        public string SINoOfEncloseVoucher { get; set; }
        public string RemaksforHR { get; set; }

        public decimal? Amount { get; set; }
        public decimal? Rent { get; set; }
    }
}
