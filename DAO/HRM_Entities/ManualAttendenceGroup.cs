using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class ManualAttendenceGroup
    {
        public int MAGId { get; set; }
        public int EmpInfoId { get; set; }
        public DateTime ManualStartDate { get; set; }
        public bool IsActive { get; set; }
        
    }
}
