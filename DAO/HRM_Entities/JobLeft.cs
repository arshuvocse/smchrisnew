using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class JobLeft
    {
        public int JobLeftId { get; set; }
        public int EmpInfoId { get; set; }
        public int CompanyInfoId { get; set; }
        public int UnitId { get; set; }
        public int DivisionId { get; set; }
        public int DeptId { get; set; }
        public int SectionId { get; set; }
        public int DesigId { get; set; }
        public int GradeId { get; set; }
        public int EmpTypeId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string ActionStatus { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string Status { get; set; }
        public DateTime RelesedOn { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string WorkYear { get; set; }
        public bool IsActive { get; set; }
    }
}
