using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class ProjectWiseEmployeeAllocationDAO
    {
        public Int32 EmpWiseProjectID { set; get; }
        public Int32 EmpInfoId { set; get; }
        public int EmployeeWiseProjectAllocationMasterId { set; get; }
        
     
       public Int32   EntryBy { set; get; } 
       public DateTime   EntryDate { set; get; } 
      public Int32    UpdateBy { set; get; }
      public DateTime UpdateDate { set; get; }

      

         
    }
}
