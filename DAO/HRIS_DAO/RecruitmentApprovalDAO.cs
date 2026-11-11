using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.util;

namespace DAO.HRIS_DAO
{
    public class RecruitmentApprovalDAO
    {
        public int RecruitmentId { get; set; }
        public int JobId { get; set; }
        public string ActionStatus { get; set; }
        public string ActionStatus2 { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string ApproveBy { get; set; }
        public DateTime ApproveDate { get; set; }
    }
}
