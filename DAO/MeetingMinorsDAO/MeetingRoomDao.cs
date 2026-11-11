using System;
using System.Security.AccessControl;

namespace DAO.MeetingMinorsDAO
{
    public class MeetingRoomDao
    {
        public int MeetingRoomId { get; set; }

        public int OfficeId { get; set; }

        public int LocationId { get; set; }

        public int FloorId { get; set; }

        public string MeetingRoomName { get; set; }

        public Decimal ? MeetingRoomCapacity { get; set; }

        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }  
    }
}