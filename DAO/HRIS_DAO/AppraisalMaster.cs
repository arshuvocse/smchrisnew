using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public  class AppraisalMaster
    {
        public int AppraisalMasterId { get; set; }
        public int BSCAppraisalMasterId { get; set; }
        public int AppraisalSelfMasterId { get; set; }
        public int BSCAppraisalSelfMasterId { get; set; }

        public int FinancialYearId { get; set; }

        public int EmpInfoId { get; set; }

        public DateTime? EntryDate { get; set; }

        public string EntryBy { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool? IsDelete { get; set; }
        public bool? IsFunctionalArea { get; set; }
        public bool? IsBehavioralArea { get; set; }

        public string DeleteBy { get; set; }

        public bool? IsApprove { get; set; }

        public string ApproveBy { get; set; }

        public DateTime? ApproveDate { get; set; }

    }


    public class BASAppraisalMaster
    {
        public int BSCAppraisalMasterId { get; set; }
        public int BSCAppraisalSelfMasterId { get; set; }

        public int FinancialYearId { get; set; }

        public int EmpInfoId { get; set; }

        public DateTime? EntryDate { get; set; }

        public string EntryBy { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool? IsDelete { get; set; }
        public bool? IsFunctionalArea { get; set; }
        public bool? IsBehavioralArea { get; set; }

        public string DeleteBy { get; set; }

        public bool? IsApprove { get; set; }

        public string ApproveBy { get; set; }

        public DateTime? ApproveDate { get; set; }

    }
    public class FuncData
    {
        public string Dimension { get; set; }
        public string DimensionStr { get; set; }
        public string ObjectiveGoal { get; set; }
        public string KPIMeasure { get; set; }
        public Decimal KpiWeight { get; set; }
        public string Initiatives { get; set; }
        public string MidYearStatus { get; set; }
        public string ResultYearEnd { get; set; }
        public string SelfMark { get; set; }
        public string SupervisorMark { get; set; }
        public DateTime? Deadline { get; set; }
        public string deadlineDate { get; set; }
    }

    public class PartBData
    {
        public string SkillInfo { get; set; }
        public string SkillType { get; set; }
        public string SupportingEmp { get; set; }
        public decimal? Score { get; set; }
        public decimal? SetScore { get; set; }
        public decimal? SelfScore { get; set; }
        public decimal? SupervisorScore { get; set; }
    } public class TrainingNeedsData
    {
        public string TrainingNeeds { get; set; }
        public string Quater { get; set; } 
    }
    public class ApprovalBSCDAO
    {
        // Serial number (SL#)
        public string SerialNumber { get; set; }

        // Employee name or identifier
        public string Employee { get; set; }

        // Comments (previous version)
        public string Comments { get; set; }

        // Date of approval
        public string ApprovalDate { get; set; }

        // Approval status
        public string ApprovalStatusText { get; set; }
    }

}
