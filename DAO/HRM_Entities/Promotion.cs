using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public int EmpInfoId { get; set; }
        public int DesigId { get; set; }
        public int SalScaleId { get; set; }
        public int NewDesigId { get; set; }
        public int NewSalScaleId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string ActionStatus { get; set; }
        public string EntryUser { get; set; }
        public DateTime EntryDate { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public bool IsActive { get; set; }
        public int GradeId { get; set; }
        public int NewGradeId { get; set; }
        public int CompanyInfoId { get; set; }
        public int UnitId { get; set; }
        public int DivisionId { get; set; }
        public int DeptId { get; set; }
        public int SectionId { get; set; }
        public string Remarks { get; set; }
    }
}
