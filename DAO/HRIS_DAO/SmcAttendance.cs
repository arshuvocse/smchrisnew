using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
  public  class SmcAttendance
    {
        public string date_row { get; set; }
        public string entry_time { get; set; }
        public string out_time { get; set; }
        public string status { get; set; }
        public string is_taken_short_leave { get; set; }
        public string is_late_attendance { get; set; }
        public string is_early_out_attendance { get; set; }
    }
}
