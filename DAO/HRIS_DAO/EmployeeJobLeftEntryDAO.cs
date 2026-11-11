using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public class EmployeeJobLeftEntryDAO
    {
       public Int32 EmployeeJobLeftId { set; get; }
       public Int32? CompanyId { set; get; }
       public Int32? EmployeeId { set; get; }
       public Int32? JobLeftTypeId { set; get; }
       public DateTime? JobLeftDate { set; get; }
       public string Reason { set; get; }

       public int EntryBy { get; set; }
       public Nullable<System.DateTime> EntryDate { get; set; }
       public int UpdateBy { get; set; }
       public Nullable<System.DateTime> UpdateDate { get; set; }

       public Nullable<bool> IsClearanceForm { get; set; }
       public Nullable<bool> IsExitInterview { get; set; }

       public Nullable<System.DateTime> SubmissionDate { get; set; }

       public int DeleteBy { get; set; }
       public DateTime DeleteDate { get; set; }
       public bool IsDelete { get; set; }
       public int BenefitId { get; set; }
       public decimal Amount { get; set; }
       public bool Active { get; set; }
       public bool IsAddition { get; set; }
       public bool IsDeduction { get; set; }
       public string AutoProcess { get; set; }
       public string ActionStatus { get; set; }


    }
}
