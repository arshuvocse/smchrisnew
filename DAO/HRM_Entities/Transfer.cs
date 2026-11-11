using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class Transfer
    {
        public int TransferId { get; set; }
        public int EmpInfoId { get; set; }
        public int CompanyInfoId { get; set; }
        public int UnitId { get; set; }
        public int DivisionId { get; set; }
        public int DeptId { get; set; }
        public int SectionId { get; set; }
        public int NewCompId { get; set; }
        public int NewUnitId { get; set; }
        public int NewDivisionId { get; set; }
        public int NewDeptId { get; set; }
        public int NewSectionId { get; set; }
        public int NewDesigId { get; set; }
        public int NewGradeId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string ActionStatus { get; set; }
        public string EntryUser { get; set; }
        public DateTime EntryDate { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
