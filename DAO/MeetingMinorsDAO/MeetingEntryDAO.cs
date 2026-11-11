using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.MeetingMinorsDAO
{
  public  class MeetingEntryDAO
    {
        public int MeetingInfoID { get; set; }

        public string MeetingCode { get; set; }

        public int? CompanyId { get; set; }

        public int? CategoryID { get; set; }

        public string MeetingPurpose { get; set; }

        public string MeetingNo { get; set; }

        public string Classification { get; set; }
        public string Title { get; set; }
        public string KeySearch { get; set; } 

        public DateTime? MeetingDate { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }
        public bool? IsOfficePremisis { get; set; }

        public bool? IsOuterPremisis { get; set; }

        public bool? IsVirtualMeeting { get; set; }

        public int? OfficeId { get; set; }

        public int? LocationId { get; set; }

        public int? FloorId { get; set; }
        public int? SubCommitteeId { get; set; }

        public int? MettingRoomId { get; set; }

        public string Location { get; set; }

        public string LocationDescription { get; set; }

        public string Remarks { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string ActionStatus { get; set; }

        public int? RefEmpId { get; set; }

        public int? RefSeqNo { get; set; }

        public int? RefMinAppCount { get; set; }

        public int? RefMinAppCountCheck { get; set; }

        public bool Isapproved { get; set; }
        public bool? IsNotice { get; set; }

        public string MailAddress { get; set; }
        public string MailBody { get; set; }
        public bool? IsSendMail { get; set; }
    }
}
