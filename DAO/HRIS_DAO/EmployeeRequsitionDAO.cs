using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class EmployeeRequsitionDAO
    {
        public int JobReqId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReqDate { get; set; }
        public int FinYearId { get; set; }
        public int JobTitleId { get; set; }
        public int GradeId { get; set; }
        public string Note { get; set; }

        public int Nos { get; set; }
        public bool IsPermanent { get; set; }
        public bool IsContract { get; set; }
        public bool IsCasual { get; set; }
        public bool IsOther { get; set; }
        public string PlaceOfPosting { get; set; }
        public string ReportingTo { get; set; }
        public int DivisionId { get; set; }
        public int WingId { get; set; }
        public int DeptId { get; set; }
        public int ReplaceEmpId { get; set; }
        public string SeparationDate { get; set; }
        public string Experience { get; set; }
        public string Skills { get; set; }
        public string Age { get; set; }
        public string Others { get; set; }
        public bool IsInternalCir { get; set; }
        public bool IsOnlineCir { get; set; }
        public bool IsSMCWeb { get; set; }
        public bool IsNewsPaper { get; set; }
        public bool IsHeadHuntFirm { get; set; }
        public string OtherCircula { get; set; }
        public bool IsOtherCircula { get; set; }
        public bool IsReplacement { get; set; }
        public bool IsBudgeted { get; set; }
        public string Justification { get; set; }
        public DateTime? ExpDateOfJoining { get; set; }

        public int BudgetId { get; set; }
        public int EmployeeId { get; set; }
        public string EmpTypeId { get; set; }

            public string OtherExperience { get; set; }
            public string Remarks { get; set; }
            public string ReqCode { get; set; }
            



            public string JobSummery { get; set; }
            public string JobTitle { get; set; }
            public int ProjectId { get; set; }
            public int SupervisorId { get; set; }
            public int OfficeId { get; set; }
            public int PlaceId { get; set; }
            public int EmployeeCategoryId { get; set; }
            public string InternalContact { get; set; }
            public string ExternalContact { get; set; }
            public string ProfCertification { get; set; }
            public string CmpSkill { get; set; }

            public string ActionStatus { get; set; }
            public string MonthContractual { get; set; }
            public string FundContractual { get; set; }
            public bool IsManagementApproved { get; set; }



           public int  EntryBy { get; set; }
          public DateTime EntryDate  { get; set; }
          public int   UpdateBy { get; set; }
           public DateTime UpdateDate  { get; set; }
           public int      DeleteBy  { get; set; }
           public DateTime DeleteDate { get; set; }
           public bool IsDelete { get; set; }
           public string PlaceOffice { get; set; }

        
    }
}
