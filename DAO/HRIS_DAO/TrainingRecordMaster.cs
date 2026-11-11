using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public class TrainingRecordMaster
    {
        public int TrainingRecordMasterId { get; set; }

        public int? CompanyId { get; set; }

        public int? FinancialYearId { get; set; }

        public int? TrainingTypeId { get; set; }

        public int? TrainingBudget2Id { get; set; }

        public string TrainingTitle { get; set; }

        public string TrainingDetails { get; set; }

        public int? TrainingOrgId { get; set; }

        public int? TrainingOrgLocation { get; set; }

        public int? TrainingVenue { get; set; }

        public decimal? TrainingCost { get; set; }

        public decimal? LogisticCost { get; set; }

        public decimal? OtherCost { get; set; }
        public decimal? GrandTotal { get; set; }
        public decimal? CostPerParticipant { get; set; }

        public string TrainingDays { get; set; }

        public int? NoOfDays { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public decimal? TotalHoure { get; set; }

        public int? EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool? IsDelete { get; set; }

        public DateTime? DeleteDate { get; set; }

        public int? DeleteBy { get; set; }

        public string TrainingRecordNo { get; set; }
        public string ActionStatus { get; set; }
    }
}
