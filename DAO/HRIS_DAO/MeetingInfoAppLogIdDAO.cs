using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class MeetingInfoAppLogIdDAO
    {
        public int Meeting_MeetingInfoAppLogId { get; set; }

        public int? MeetingInfoID { get; set; }

        public int? PreEmpInfoId { get; set; }

        public int? ForEmpInfoId { get; set; }

        public int? Version { get; set; }

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public string ActionStatus { get; set; }

        public string Comments { get; set; }
    }
}
