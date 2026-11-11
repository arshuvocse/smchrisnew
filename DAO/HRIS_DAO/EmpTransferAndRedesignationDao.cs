using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public class EmpTransferAndRedesignationDao
    {
       public string ActionStatus { get; set; }
        public int EmpTransferAndRedesignationId { get; set; }
        public Nullable<int> CompanyId { get; set; }

        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> FinancialYearId { get; set; }
        public Nullable<int> EmpTypeId { get; set; }
        public Nullable<bool> IsOnlyTransfer { get; set; }
       
       
        public Nullable<bool> IsInterCompanyTransfer { get; set; }

        public Nullable<int> PrevEmpReportingBodyId { get; set; }

        public Nullable<int> NewEmpReportingBodyId { get; set; }
        public Nullable<int> OldReportingBodyID { get; set; }
        public Nullable<int> NewCompanyId { get; set; }
        public Nullable<int> NewSalaryLocationId { get; set; }
        public Nullable<int> NewJobLocationId { get; set; }
        public Nullable<int> NewDivisionId { get; set; }
        public Nullable<int> NewWingId { get; set; }
        public Nullable<int> NewSectionId { get; set; }
        public Nullable<int> NewSubSectionId { get; set; }
        public Nullable<int> OldCompanyId { get; set; }
        public Nullable<int> OldSalaryLocationId { get; set; }
        public Nullable<int> OldJobLocationId { get; set; }
        public Nullable<int> OldDivisionId { get; set; }
        public Nullable<int> OldWingId { get; set; }
        public Nullable<int> OldSectionId { get; set; }
        public Nullable<int> OldSubSectionId { get; set; }
        public string EntryBy { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.DateTime> EffectiveDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }



       

        public Nullable<int> NewDepartmentId { get; set; }
        public Nullable<int> OldDepartmentId { get; set; }


        public Nullable<int> EmpInfoId { get; set; }
        public string Remarks { get; set; }
        public string AutoProcess { get; set; }
        public string EmployeeCode { get; set; }
        public bool? IsReappointment { get; set; }
      
    }


   public class  EmpSpecialTransferDAO
   {
       public int EmpSpecialTransferId { get; set; }

       public int? EmpTransferAndRedesignationId { get; set; }

       public bool? SpecialTransfer { get; set; }

       public bool? RegularTransfer { get; set; }

       public bool? FullTransfer { get; set; }

       public bool? SalaryTransfer { get; set; }
       public bool? RecordUpdateTypeSalaryTransfer { get; set; }

       public bool? OnlyView { get; set; }

       public bool? EditView { get; set; }
       public bool? IsSMCRecordView { get; set; }
       public bool? IsELRecordView { get; set; }
       public Nullable<int> EmployeeId { get; set; }
       public Nullable<int> NewComId { get; set; }
       public Nullable<int> NewEmployeeId { get; set; }
       public Nullable<int> OnlyViewComId { get; set; }
       public Nullable<int> EditViewComId { get; set; }


   }

}
