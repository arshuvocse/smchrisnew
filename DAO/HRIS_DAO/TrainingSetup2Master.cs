using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
  public   class TrainingSetup2Master
    {

        public int TrainingSetup2MasterId { get; set; }

        public string TrainingTitle { get; set; }

        public string TrainingDetails { get; set; }

        public bool? FromReq { get; set; }

        public int? TrainingReq2DetailsId { get; set; }

        public int? TrainingBudget2DetailsId { get; set; }

        public int? CompanyId { get; set; }

        public int? FinancialYearId { get; set; }

        public string EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool? IsDelete { get; set; }
        public bool? HasEvauation { get; set; }

        public string DeleteBy { get; set; }
        public int Duration { get; set; }
        public int TrainingOrgId { get; set; }
        public int OfficeBranchId { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
