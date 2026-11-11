using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class OtherDeduction
    {
        public int ODId { get; set; }
        public int EmpId { get; set; }
        public int CompanyInfoId { get; set; }
        public int UnitId { get; set; }
        public int DivisionId { get; set; }
        public int DeptId { get; set; }
        public int SectionId { get; set; }
        public int DesigId { get; set; }
        public int GradeId { get; set; }
        public int EmpTypeId { get; set; }
        public int EmpGradeId { get; set; }
        public decimal ODAmount { get; set; }
        public DateTime ODEffectiveDate { get; set; }
        public string ODReason { get; set; }
        public string ActionStatus { get; set; }
        public string EntryUser { get; set; }
        public DateTime EntryDate { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
