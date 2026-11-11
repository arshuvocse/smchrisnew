using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class EmployeeCategory
    {

        public int EmpCategoryId { get; set; }
        public string EmpCategoryCode { get; set; }
        public string EmpCategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
