using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class EmployeeType
    {
        public int EmpTypeId { get; set; }
        public string EmpType { get; set; }
        public bool IsActive { get; set; }
    }
}
