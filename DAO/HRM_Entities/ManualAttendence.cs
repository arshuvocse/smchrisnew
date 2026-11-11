using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class ManualAttendence
    {
        public int MAttendenceId { get; set; }
        public int EmpInfoId { get; set; }
        public DateTime ATTDate { get; set; }
        public int ShiftId { get; set; }
        public string ShiftInTime { get; set; }
        public string ShiftOutTime { get; set; }
        public string ATTStatus { get; set; }
        public string EntryReason { get; set; }
        public string ActionStatus { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string Approveby { get; set; }
        public DateTime ApproveDate { get; set; }
        public string DayName { get; set; }
        public string OverTimeDuration { get; set; }
        public bool IsActive { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
