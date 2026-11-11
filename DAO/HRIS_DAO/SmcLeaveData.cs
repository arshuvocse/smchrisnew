using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class SmcLeaveData
    {
        public string leave_record_id { get; set; }
        public string employeeID { get; set; }
        public string benifited_year { get; set; }
        public string leave_type_id { get; set; }
        public string previous_year_balance { get; set; }
        public string aquired_leave { get; set; }
        public string total_available_leave { get; set; }
        public string allowable_leave { get; set; }
        public string taken_leave { get; set; }
        public string taken_against_short_leave { get; set; }
        public string taken_against_late_in { get; set; }
        public string prev_year_cf { get; set; }
        public string encashed_leave { get; set; }
        public string current_year_balance { get; set; }
        public string total_balance { get; set; }
        public string stop_sick_leave_calculation { get; set; }
        public string have_child { get; set; }
        public string EmploymentTypeID { get; set; }
        public string year_start_date { get; set; }
        public string year_end_date { get; set; }
        public string current_plan_id { get; set; }
        public string create_date { get; set; }
        public string is_balance_sheet_completed { get; set; }
        public string oldEmpNoRef { get; set; }
    }
}
