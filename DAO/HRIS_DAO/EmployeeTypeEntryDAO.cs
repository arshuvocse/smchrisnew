using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class EmployeeTypeEntryDAO
    {
        public Int32 EmpTypeId { set; get; }
        public Int32 CompanyID { set; get; }
        public string EmpType { set; get; } 
        
       public bool   IsActive { set; get; } 
       public Int32   EntryBy { set; get; } 
       public DateTime   EntryDate { set; get; } 
      public Int32    UpdateBy { set; get; }
      public DateTime UpdateDate { set; get; }

     

         
    }
}
