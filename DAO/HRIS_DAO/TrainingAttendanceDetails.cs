using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public  class TrainingAttendanceDetails
    {

        public int TrainingAttendanceDetailsId { get; set; }

        public int TrainingAttendanceMasterId { get; set; }

        public int EmpInfoId { get; set; }

        public int TrainingBudgetAllocationDetailsId { get; set; }
        public int TrainingRequisitionDetailsId { get; set; }

      
        public bool IsPresent { get; set; }
       
    }
}
