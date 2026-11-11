using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public class TrainingBudgetAllocationDetails
    {
        public int TrainingBudgetAllocationDetailsId { get; set; }

        public int TrainingBudgetAllocationId { get; set; }

        public int TrainingBudgetDetailsDptId { get; set; }
        public int TrainingBudgetDetailsGradeId { get; set; }
        public int TrainingBudgetDetailsEmpId { get; set; }

        public int EmpInfoId { get; set; }

        public int Quater { get; set; }
        public bool IsActive { get; set; }
    }
}
