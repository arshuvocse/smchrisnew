using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class HolidayReplace
    {
        public int WHRId { get; set; }
        public int EmpInfoId { get; set; }
        public string EmpMasterCode { get; set; }
        public string EmpName { get; set; }
        public DateTime WeekHolidaydate { get; set; }
        public string WeekHolidayDayName { get; set; }
        public DateTime AlternativeDate { get; set; }
        public string AlternativeDayName { get; set; }
        public string EntryUser { get; set; }
        public DateTime EntryDate { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string ActionStatus { get; set; }
        public bool IsActive { get; set; }
    }
}
