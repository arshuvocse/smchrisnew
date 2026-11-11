using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DeaprtmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string DeptShortName { get; set; }
        public int DivisionId { get; set; }
        public bool IsActive { get; set; }
    }
}
