using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class EmpIncrement
    {
        public int IncrementId { get; set; }
        public string EmpMasterCode { get; set; }
        public string EmpName { get; set; }
        public string Designation { get; set; }
        public string EmpGrade { get; set; }
        public string DeptName { get; set; }
        public string SalaryScale { get; set; }
        public int DesigId { get; set; }
        public int DeptId { get; set; }
        public int GradeId { get; set; }
        public int SalScaleId { get; set; }
        public string NoOfIncrement { get; set; }
        public string IncrementAmount { get; set; }
        public string TotalGrossSalary { get; set; }
        public string PromotionalStatus { get; set; }
        public string NewDesignation { get; set; }
        public string Comments { get; set; }
        public DateTime IncEfectDate { get; set; }
        public bool IsActive { get; set; }
    }
}
