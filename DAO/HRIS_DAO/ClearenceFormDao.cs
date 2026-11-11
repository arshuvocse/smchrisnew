using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class ClearenceFormDao
    {
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public Int32 EmployeeJobLeftId { get; set; }
        public Int32 DepartmentId { get; set; }
        public Int32 EntryByPersonId { get; set; }
        public DateTime JoiningDate { get; set; }
        public int DivisionId { get; set; }
        public int? DesignationId { get; set; }
        public int? SalaryGradeId { get; set; }

        public int? SalaryLoationId { get; set; }
        public string Description { get; set; }
        public string Recommend { get; set; }
        public string Remarks { get; set; }
        public string ActionStatus { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
