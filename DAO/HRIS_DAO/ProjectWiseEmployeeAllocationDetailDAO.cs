using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class ProjectWiseEmployeeAllocationDetailDAO
    {
        public Int32 EmpWiseProjectDetailID { set; get; }
        public Int32 ProjectId { set; get; }
        public bool IsActive { set; get; }
        public bool IsMaster { set; get; }


        public Int32 EmployeeWiseProjectAllocationMasterId { set; get; } 
       

      

         
    }
}
