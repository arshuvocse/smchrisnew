using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class IncrementDao
    {
        public int IncrementId { get; set; }

       public Int32 CompanyId { get; set; }
       public Int32 FinancialYearId { get; set; }
       public Int32 IncrementTypeId { get; set; }
       public Int32 DivisionId { get; set; }
       public Int32 DivisionWId { get; set; }
       public Int32 EmployeeId { get; set; }
       public string EmployeeCode { get; set; }
       public string EmployeeName { get; set; }
       public Int32 DesignationId { get; set; }
       public Int32 DepartmentId { get; set; }
       public Int32 SectionId { get; set; }
       public Int32 SubSectionId { get; set; }
       public Int32 SalaryLoationId { get; set; }
       public Int32 JobLocationId { get; set; }
       public Int32 EmpTypeId { get; set; }
       public DateTime JoiningDate { get; set; }
       public Int32 ServiceLength { get; set; }
       public Int32 SalaryGradeId { get; set; }
       public Int32 CurrentStepId { get; set; }
       public Int32 IncrementalStepId { get; set; }
       public Decimal FeedSalary { get; set; }

       public string ActionStatus { get; set; }
       public int EntryBy { get; set; }
       public DateTime EntryDate  { get; set; }
       public int UpdateBy { get; set; }
       public DateTime UpdateDate { get; set; }
       public DateTime EffectiveDate { get; set; }
       public bool IsDelete { get; set; }
       public int DeleteBy { get; set; }


       public DateTime DeleteDate { get; set; }
       public String AutoProcess { get; set; }


    }


    

}
