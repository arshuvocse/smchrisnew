using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class EmployeeGrade
    {
        public int GradeId { get; set; }
        public string GradeCode { get; set; }
        public string GradeName { get; set; }
        public string GradeType { get; set; }
        public bool IsActive { get; set; }
    }
}
