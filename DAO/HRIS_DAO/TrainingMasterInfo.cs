using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class TrainingMasterInfo
    {
        public int TrainingMasterId { get; set; }
        public int CompanyId { get; set; }
        public int FinancialYearId { get; set; }
        public string TrainingTitle { get; set; }
        public string TrainingDetails { get; set; }
        public int TrainingBudgetAllocationId { get; set; }
        public string TrainingSetupNumber { get; set; }
        public string SpecifcFor { get; set; }
        public int OfficeBranchId { get; set; }

        public int Quater { get; set; }
        public int TrainingOrgId { get; set; }
        public DateTime TrainingStart { get; set; }
        public DateTime TrainingEnd { get; set; }
        public int TrainingDuration { get; set; }
        public bool TrainingEvaluation { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public int TrainingRequisitionMasterId { get; set; }
    }
}
