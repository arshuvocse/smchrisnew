using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class SalaryStepInformationDao
    {
        public Int32 SalaryStepId { set; get; }
        public Int32 SalaryGradeId { set; get; }
        public string SalaryStepName { set; get; }
        public decimal GrossAmount { get; set; }
        public decimal BasicAmount { get; set; }
        public bool IsActive { set; get; }
        public string ApprovalStatus { set; get; }
        public string Description { set; get; }
        public string Remarks { set; get; }
        public string EntryBy { set; get; }
        public DateTime EntryDate { set; get; }
        public string UpdateBy { set; get; }
        public DateTime UpdateDate { set; get; }
        public string ApproveBy { set; get; }
        public DateTime ApproveDate { set; get; } 
    }
}
