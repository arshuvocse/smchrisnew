using System;
using System.Data;

namespace DAO.MeetingMinorsDAO
{
    public class MemberSetupMaster
    {
        public int BMemberSetupMasterID { get; set; }

        public int? CompanyId { get; set; }
        public int CategoryID { get; set; }

        public string Description { get; set; }
        public bool isActive { get; set; }
        public string BoardMemberName { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string UpdateBy { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}