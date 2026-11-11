using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class ShiftWiseGroup
    {
        public int GSAId { get; set; }
        public int GroupId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int ShiftId { get; set; }
        public TimeSpan ShiftInTime { get; set; }
        public TimeSpan ShiftOutTime { get; set; }
        public string EntryUser { get; set; }
        public DateTime EntryDate { get; set; }
        public string ApproveUser { get; set; }
        public DateTime ApproveDate { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        
    }
}
