using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.MeetingMinorsDAO
{
  public  class MiscellaneousInfoRoutingPathDAO
    {
        public int MiscellaneousInfoRoutingPathID { get; set; }

        public int? MiscellaneousInfoId { get; set; }

        public int? EmpInfoId { get; set; }

        public int? Seq_No { get; set; }

        public bool? CanEdit { get; set; }

        public bool? IsEmailNotification { get; set; }

        public bool? IsSMSNotification { get; set; }

        public bool? IsMinimumApprovalPerson { get; set; }
    }
}
