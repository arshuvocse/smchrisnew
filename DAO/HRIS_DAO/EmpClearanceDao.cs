using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public class EmpClearanceDao
    {
       public int EmpClearanceId { get; set; }
       public int EmpInfoId { get; set; }
       public DateTime? MobIssuDate { get; set; }
       public decimal MobActualPrice { get; set; }
       public decimal MoDABeforeOne { get; set; }
       public decimal MoDABeforeOnetotwo { get; set; }
       public decimal MoDAAboveTwo { get; set; }
       public string MoRemarks { get; set; }
       public DateTime? DBIssuDate { get; set; }
       public decimal DBActualPrice { get; set; }
       public decimal DBDeductionAmount { get; set; }
       public string DBRemark { get; set; }
       public decimal PIActualCost { get; set; }
       public decimal PIDeductionAmount { get; set; }
       public string PIRemark { get; set; }
       public string IDCard { get; set; }
       public string IDCardRemark { get; set; }
       public decimal MarketDues { get; set; }
       public string MarketRemarks { get; set; }
       public decimal TotalDeductionAmount { get; set; }
       public string ToDeducAmtRemark { get; set; }
       public int EntryBy { get; set; }
       public DateTime EntryDate { get; set; }
       public int UpdateBy { get; set; }
       public DateTime UpdateDate { get; set; }
       public  string TickMark { get; set; }

       public int ClearenceId { get; set; }

    }
}
