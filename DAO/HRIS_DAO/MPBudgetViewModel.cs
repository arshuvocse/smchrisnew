using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class MPBudgetViewModel
    {
        public int MPBudgetMasterId { get; set; }
        public string BudgetCode { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public int FinancialYearId { get; set; }
        public int DesignationId { get; set; }
        public int ExistingEmployee { get; set; }
        public decimal ExistingSalary { get; set; }
        public int SalaryGradeId { get; set; }
        public int SalaryStepId { get; set; }
        public int GExistingEmployee { get; set; }
        public decimal GExistingSalary { get; set; }
        public int EmployeeRequisition { get; set; }
        public decimal ReqApproxSalary { get; set; }
        public decimal ReqTotalSalary { get; set; }
        public int QuarterId  { get; set; }

        public string CompanyShortName { get; set; }
        public string DepartmentName { get; set; }
        public string FinancialYearDesc { get; set; }
        public string Designation { get; set; }
        public string GradeName { get; set; }
        public string SalaryStepName { get; set; }
        public string QuarterName { get; set; }
    }
}
