using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class InterviewBoardSetupMasterDao
    {
        public Int32 SetupMasterId { set; get; }
        public Int32 CompanyId { set; get; }
        public Int32 JobTitleId { set; get; }
        public Int32 InterviewNoId { set; get; }
        public string InterviewTime { set; get; }
        public string Vanue { set; get; }
        public bool IsEmployee { set; get; }
        public bool IsGuest { set; get; }
        public DateTime InterviewDate { set; get; }
        public string EntryBy { set; get; }
        public string ApprovalStatus { set; get; }
        public DateTime EntryDate { set; get; }
        public string UpdateBy { set; get; }
        public DateTime UpdateDate { set; get; }
        public string ApproveBy { set; get; }
        public DateTime ApproveDate { set; get; } 
    }
}
