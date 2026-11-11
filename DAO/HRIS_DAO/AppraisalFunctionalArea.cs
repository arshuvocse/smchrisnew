using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class AppraisalFunctionalArea
    {

        public int AppraisalFucAreaId { get; set; }

        public int? AppraisalMasterId { get; set; }
        public int? AppraisalSelfMasterId { get; set; }

        public string KpiInfo { get; set; }

        public decimal? KpiWeight { get; set; }
        public decimal? KpiWeightPer { get; set; }

        public DateTime? Deadline { get; set; }

        public string MidYearStatus { get; set; }
        public string Comments { get; set; }
        public string SupervisorComments { get; set; }

        public string ResultYearEnd { get; set; }

        public decimal? SelfMark { get; set; }

        public decimal? SupervisorMark { get; set; }

        public decimal? Target { get; set; }
        public decimal? TargetPer { get; set; }
        public bool? IsActive { get; set; }
        public int AppraisalSelfFucAreaId { get; set; }
    } public class AppraisalFunctionalAreaBSC
    {

        public int AppraisalFucAreaId { get; set; }

        public int? AppraisalMasterId { get; set; }
        public int? AppraisalSelfMasterId { get; set; }

        public string KpiInfo { get; set; }

        public decimal? KpiWeight { get; set; }
        public decimal? KpiWeightPer { get; set; }

        public DateTime? Deadline { get; set; }

        public string MidYearStatus { get; set; }

        public string ResultYearEnd { get; set; }

        public decimal? SelfMark { get; set; }
       

        public decimal? SupervisorMark { get; set; }

        public decimal? Target { get; set; }
        public decimal? TargetPer { get; set; }
        public bool? IsActive { get; set; }
        public int BSCAppraisalSelfFucAreaId { get; set; }
        public int Dimension { get; set; }
        public string DimensionStr { get; set; }
        public string ObjectiveGoal { get; set; }
        public string SelfAssessment { get; set; }
        public string SupervisorAssessment { get; set; }
        public string KPIMeasure { get; set; }
        public string Initiatives { get; set; }
        public string CommentsFunc { get; set; }
        public string SupervisorComments { get; set; }
         
    }
}
