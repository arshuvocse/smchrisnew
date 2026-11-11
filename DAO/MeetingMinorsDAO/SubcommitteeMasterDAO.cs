using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.MeetingMinorsDAO
{
  public  class SubcommitteeMasterDAO
    {
        public int  SubcommitteeMasterId { get; set; }

        public int? CompanyId { get; set; }

        public int? CategoryID { get; set; }

        public string SubCommitteeName { get; set; }

        public string Remarks { get; set; }
        public string ActionStatus { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
