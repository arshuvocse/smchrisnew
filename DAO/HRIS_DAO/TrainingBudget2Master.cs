using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class TrainingBudget2Master
    {
        public int TrainingBudget2Id { get; set; }

        public int? CompanyId { get; set; }

        public int? FinancialYearId { get; set; }

        public decimal? TotalYearlyBudgetCost { get; set; }

        public decimal? TotalBudget { get; set; }

        public string EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool? IsDelete { get; set; }

        public string DeleteBy { get; set; }

        public DateTime? DeleteDate { get; set; }
        public string ActionStatus { get; set; }
    }
}
