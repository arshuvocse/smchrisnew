using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HealthCare_Dao
{
    public class BillSettlement
    {

        public int BillSettlmentId { get; set; }

        public int? ReimbursFromMasterId { get; set; }

        public string ClaimNo { get; set; }
        public string MainRemarks { get; set; }

        public DateTime? SettlementDate { get; set; }

        public bool? RecommendedPayment { get; set; }

        public string PayableFrom { get; set; }

        public decimal? OPDIPDBalance { get; set; }
    
        public decimal? OtherBalance { get; set; }

        public decimal? PaybleAmount { get; set; }
        public decimal? AddvanceTK { get; set; }

        public decimal? RemainBalance { get; set; }


        public string PaymentType { get; set; }

        public DateTime? CheckDate { get; set; }

        public int? EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }


        public bool? IsExtraAllocate { get; set; }

        public DateTime? CashDate { get; set; }

        public decimal? ExtraAllocateTK { get; set; }
        public decimal? CurrentBlnce { get; set; }
        public decimal? ApplicationAmount { get; set; }
        public decimal? RemaiingBlnce { get; set; }

        public int? BankId { get; set; }
        public string AccountNo { get; set; }


    }
}
