using System;
using System.Security.AccessControl;

namespace DAO.MeetingMinorsDAO
{
    public class MemberDetailsInfoNewDAO
    {
        public int MemberSetupDetailsID { get; set; }
        public int MemberSetupMasterID { get; set; }
        public int? MeetingMemberTypeId { get; set; }
        public int? CompanyId { get; set; }
        public int? OrderNo { get; set; }
        
        public string MemberType { get; set; }
        public string Name { get; set; } 
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public DateTime? MembershipDate { get; set; }
        public DateTime? JoiningDate { get; set; }

        public string Note { get; set; }

    }
}