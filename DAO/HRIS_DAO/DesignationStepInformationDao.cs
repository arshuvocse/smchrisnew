using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class DesignationStepInformationDao
    {
        public Int32 DesignationStepId { set; get; }
        public string DesignationStepName { set; get; }
        public string Remarks { set; get; }
        public string EntryBy { set; get; }
        public string ApprovalStatus { set; get; }
        public bool IsActive { set; get; }
        public DateTime EntryDate { set; get; }
        public string UpdateBy { set; get; }
        public DateTime UpdateDate { set; get; }
        public string ApproveBy { set; get; }
        public DateTime ApproveDate { set; get; } 
    }
}
