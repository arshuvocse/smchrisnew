using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRM_Entities
{
    public class ExpenseEntryDao
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
        public string ActionStatus { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Decimal ExpenseAmount { get; set; }
      
    }
}
