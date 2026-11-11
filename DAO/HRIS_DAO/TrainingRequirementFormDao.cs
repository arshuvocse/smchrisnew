using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class TrainingRequirementFormDao
    {
        public int TrainingRequirementId { get; set; }
        public int CompanyId { get; set; }
        public int DivisionId { get; set; }
        public int DivisionWId { get; set; }
        public int DepartmentId { get; set; }
        public int SectionId { get; set; }
        public int SubSectionId { get; set; }
        public int FinYearId { get; set; }
        public int TrainingTopicId { get; set; }
        public bool Q1 { get; set; }
        public bool Q2 { get; set; }
        public bool Q3 { get; set; }
        public bool Q4 { get; set; }
        public string TrainingExp { get; set; }
        public string Remarks { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsActive { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApproveDate { get; set; }
        public string ApprovalStatus { get; set; }
    }
}
