using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class Increment
    {
        public int IncrementId { get; set; }
        public int EmpInfoId { get; set; }
        public int CompanyInfoId { get; set; }
        public int UnitId { get; set; }
        public int DivisionId { get; set; }
        public int SectionId { get; set; }
        public int DesigId { get; set; }
        public int DeptId { get; set; }
        public int SalScaleId { get; set; }
        public decimal IncrementAmount { get; set; }
        public decimal TotalGrossSalary { get; set; }
        public decimal PreviousGrossSalary { get; set; }
        public string ActionStatus { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ActiveDate { get; set; }
        public string ApprovedBy { get; set; }
        public string IncrementRemarks { get; set; }
        public DateTime ApprovedDate { get; set; }
        public bool IsActive { get; set; }
        public int? PromotionId { get; set; }
        public string Remarks { get; set; }
    }
}
