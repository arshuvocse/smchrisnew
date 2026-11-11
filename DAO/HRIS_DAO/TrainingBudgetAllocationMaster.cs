using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class TrainingBudgetAllocationMaster
    {

        public int TrainingBudgetAllocationId { get; set; }

        public bool ForDepartment { get; set; }

        public bool ForGrade { get; set; }

        public bool ForEmployee { get; set; }

        public int TrainingBudgetDetailsId { get; set; }

        public string EntryBy { get; set; }
        public int TrainingBudgetMasterId { get; set; }
        

        public DateTime? EntryDate { get; set; }

        public int FinancialYearId { get; set; }

        public string UpdateBy { get; set; }

        public DateTime UpdateDate { get; set; }

        public string Quater { get; set; }
    }
}
