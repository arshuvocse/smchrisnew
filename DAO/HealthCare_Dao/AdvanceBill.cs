using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HealthCare_Dao
{
   public class AdvanceBill
    {
        public int AdvanceBillId { get; set; }

        public int? ReimbursFromMasterId { get; set; }

        public string RequitisionNo { get; set; }

        public int? CompanyId { get; set; }

        public int? FinancialId { get; set; }

        public bool? IsOPD { get; set; }
        public bool? IsIPD { get; set; }
        public bool? IsSpecial { get; set; }

        public decimal? Amount { get; set; }

        public string Remarks { get; set; }
        public string CarryPerson { get; set; }
        public int? EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        public int? EmpInfoId { get; set; }

        public string MemoImage { get; set; }

        public string ImagePath { get; set; }
    }
}
