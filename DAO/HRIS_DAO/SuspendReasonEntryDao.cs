using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class SuspendReasonEntryDao
    {
        public Int32 SuspendReasonEntryId { set; get; }
        public string SuspendReasonEntry { set; get; } 
       public bool   IsActive { set; get; } 
       public Int32   EntryBy { set; get; } 
       public DateTime   EntryDate { set; get; } 
      public Int32    UpdateBy { set; get; }
      public DateTime UpdateDate { set; get; }

      public Int32 DeleteBy { set; get; }
      public DateTime DeleteDate { set; get; }

      public bool IsDelete { set; get; }

      public int CompanyId { set; get; }

      public bool IsSuspend { set; get; }
      public bool IsDisciplinary { set; get; }
     

         
    }
}
