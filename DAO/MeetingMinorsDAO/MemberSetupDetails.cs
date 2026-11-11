using System;
using System.Security.AccessControl;

namespace DAO.MeetingMinorsDAO
{
    public class MemberSetupDetails
    {
        public int BMemberSetupDetailsID { get; set; }
        public int BMemberSetupMasterID { get; set; }
        public int? MeetingMemberTypeId { get; set; }
        public int? CompanyId { get; set; }
        
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