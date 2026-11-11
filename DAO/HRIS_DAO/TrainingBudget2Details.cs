using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class TrainingBudget2Details
    {
        public int TrainingBudget2DetailsId { get; set; }

        public int? TrainingBudget2Id { get; set; }

        public string TrainingTitle { get; set; }

        public string ExpectedResult { get; set; }

        public int? QuaterId { get; set; }

        public string Quater { get; set; }
        public string Grade { get; set; }
        public string EmpCategoryId { get; set; }

        public int? MonthId { get; set; }

        public bool? IsInternal { get; set; }

        public bool? IsExternal { get; set; }

        public bool? IsForeign { get; set; }

        public bool? IsLocal { get; set; }

        public int? TotalParticipant { get; set; }

        public decimal? BudgetCostParticipant { get; set; }

        public decimal? Budget { get; set; }

        public string Referance { get; set; }

        public string Remarks { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string DeleteBy { get; set; }
        public DateTime DeleteDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
