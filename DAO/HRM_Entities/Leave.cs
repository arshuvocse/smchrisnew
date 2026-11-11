using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class Leave
    {
        public int LeaveId { get; set; }
        public string LeaveCode { get; set; }
        public string LeaveName { get; set; }
        public bool IsActive { get; set; }
    }
}
