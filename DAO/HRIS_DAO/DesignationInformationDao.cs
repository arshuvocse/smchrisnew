using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class DesignationInformationDao
    {
        public Int32 DesignationId { set; get; }
        public Int32 SalaryGradeId { set; get; }
        public Int32 CompanyId { set; get; }
        public Int32 SalaryStepId { set; get; }
        public string Designation { set; get; }
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
     public int   DesignationStepId { set; get; } 
    }
}
