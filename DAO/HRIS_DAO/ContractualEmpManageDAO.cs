using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
  public  class ContractualEmpManageDAO
    {
        public int ContractualEmpManageId { get; set; }
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<bool> IsExtension { get; set; }
        public Nullable<bool> IsRenew { get; set; }
        public Nullable<bool> IsPermanentToContractual { get; set; }
        public Nullable<bool> IsContractualToPermanent { get; set; }
        public Nullable<bool> IsSMCFundedProjectstoSMCContract { get; set; }
        public Nullable<bool> IsSMCContracttoSMCFundedProjects { get; set; }
        public Nullable<bool> isToProject { get; set; }
        public Nullable<bool> isReappointment { get; set; }
        public Nullable<bool> IsRedesignation { get; set; }
        public Nullable<bool> IsSalaryIncrement { get; set; }
        public Nullable<bool> IsNoIncrement { get; set; }
        public Nullable<bool> IsFacilityIncluded { get; set; }
        public Nullable<bool> IsNoFacility { get; set; }
        public string Remarks { get; set; }
        public string TypeOfPromotion { get; set; }
        public string EntryBy { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }




        public Nullable<System.DateTime> ExtensionFromDate { get; set; }
        public Nullable<System.DateTime> ExtensionToDate { get; set; }
        public Nullable<System.DateTime> RenewStartDate { get; set; }
        public Nullable<System.DateTime> RenewToDate { get; set; }
        public Nullable<System.DateTime> PermanentToContractualEffectiveDate { get; set; }
        public Nullable<System.DateTime> PermanentToContractualEndDate { get; set; }
        public Nullable<System.DateTime> ContractualToPermanentDate { get; set; }
        public string ActionStatus { get; set; }
        public string FromProject { get; set; }
        public string ToProject { get; set; }
        public bool IsDelete { get; set; }
        public int DeleteBy { get; set; }
        public DateTime DeleteDate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public Nullable<int> EmployeeCode { get; set; }
        public Nullable<int> EmployeeName { get; set; }
        public Nullable<int> ContractPreiod { get; set; }
        public Nullable<int> DesignationId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> SectionId { get; set; }
        public Nullable<int> SubSectionId { get; set; }
        public Nullable<int> SalaryLoationId { get; set; }
        public Nullable<int> JobLocationId { get; set; }
        public Nullable<int> EmpTypeId { get; set; }
        public Nullable<int> DivisionWId { get; set; }
        public Nullable<int> DivisionId { get; set; }
        public bool? AutoProcess { get; set; }

        public Nullable<int> SalaryGradeId { get; set; }
        public Nullable<int> SalaryStepId { get; set; }

    }
}
