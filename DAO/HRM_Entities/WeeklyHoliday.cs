using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class WeeklyHoliday
    {
        public int WeeklyHolidayId { get; set; }
        public int EmpId { get; set; }
        public string FirstHolidayName { get; set; }
        public string SecondHolidayName { get; set; }
        public string DayQty { get; set; }
        public bool IsActive { get; set; }
    }
}
