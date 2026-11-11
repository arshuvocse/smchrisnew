using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class SalaryStartStop
    {
        public int SalaryStopId { get; set; }
        public int FyrId { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string LockedBy { get; set; }
        public DateTime LockedDate { get; set; }
        public string UnlockedBy { get; set; }
        public DateTime UnlockedDate { get; set; }
        public string Status { get; set; }


    }
}
