using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.MeetingMinorsDAO
{
   public class MeetingInfoDetailDAO
    {

        public int Meeting_MeetingInfoDetailId { get; set; }

        public int? MeetingInfoID { get; set; }

        public string Type { get; set; }

        public int? EmpInfoId { get; set; }

        public string EmpMasterCode { get; set; }
        public string IsBoardMember { get; set; }
        public string BMemberSetupDetailsID { get; set; }
        public string EmpName { get; set; }

        public string Designation { get; set; }

        public bool? NotificationEmail { get; set; }

        public bool? NotificationSMS { get; set; }

        public string Position { get; set; }

    }
}
