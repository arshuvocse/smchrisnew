using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class UnRestShift
    {
        public int UnRestShiftId { get; set; }
        public string ShiftName { get; set; }
        public int ConsideredInMin { get; set; }
        public TimeSpan BreakStart { get; set; }
        public TimeSpan BreakEnd { get; set; }
        public TimeSpan ShiftInTime { get; set; }
        public TimeSpan ShiftOutTime { get; set; }
        public int ShiftId { get; set; }
        public DateTime EffectiveDate { get; set; }
        
    }
}
