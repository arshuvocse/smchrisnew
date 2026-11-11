using System;
using System.Security.AccessControl;

namespace DAO.MeetingMinorsDAO
{
    public class FloorDAO
    {
        public int FloorId { get; set; }

        public int OfficeId { get; set; }

        public int LocationId { get; set; }



        public string FloorName { get; set; }

       

        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }  
    }
}