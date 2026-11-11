using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class Attendence
    {
        public int AttendenceId { get; set; }
        public int EmpId { get; set; }
        public DateTime ATTDate { get; set; }
        public string DayName { get; set; }
        public string ShiftId { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }
        public TimeSpan InTime { get; set; }
        public TimeSpan OutTime { get; set; }
        public TimeSpan ComOutTime { get; set; }
        public TimeSpan OTDuration { get; set; }
        public TimeSpan ComOTDuration { get; set; }
        public TimeSpan DutyDuration { get; set; }
        public TimeSpan ComDutyDuration { get; set; }
        public string ATTStatus { get; set; }
        public string Remarks { get; set; }
        public string Date { get; set; }
    }
}
