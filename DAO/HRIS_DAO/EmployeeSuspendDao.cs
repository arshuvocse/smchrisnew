using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class EmployeeSuspendDao
    {

        public int SuspendId { get; set; }
        public int EmpInfoId { get; set; }
        public int CompanyInfoId { get; set; }
        public int? DivisionId { get; set; }
        public int? DivisionWId { get; set; }
        public int? SalaryLoationId { get; set; }
        public int? JobLocationId { get; set; }
        public int? DeptId { get; set; }
        public int? SectionId { get; set; }
        public int? SubSectionId { get; set; }
        public int? DesigId { get; set; }
        public int? EmpTypeId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EffectiveToDate { get; set; }
        public string ActionStatus { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public bool isRelease { get; set; }
        public string ReleasedBy { get; set; }
        public bool? IsActive { get; set; }
        public DateTime RelesedOn { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public bool isSuspensionLetter { get; set; }
        public bool isWithPay { get; set; }
        public bool isWithoutPay { get; set; }
        public string ReleaseExplain { get; set; }
        public string ReleaseRemarks { get; set; }
        public int? ReasonId { get; set; }
        public int FinancialYearId { get; set; }
        public string EmpCode { get; set; }
        public Int32 UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string AutoProcess { get; set; }

    }
}
