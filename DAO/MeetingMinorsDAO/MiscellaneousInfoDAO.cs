using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.MeetingMinorsDAO
{
   public class MiscellaneousInfoDAO
    {
        public int MiscellaneousInfoId { get; set; }

        public int? CompanyId { get; set; }

        public string Title { get; set; }

        public string Purpose { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }
        public string KeySearch { get; set; }

        public string ActionStatus { get; set; }
        public string ReturnComments { get; set; }

        public int? RefEmpId { get; set; }

        public int? RefSeqNo { get; set; }

        public int? RefMinAppCount { get; set; }

        public int? RefMinAppCountCheck { get; set; }

        public bool Isapproved { get; set; }

    }
}
