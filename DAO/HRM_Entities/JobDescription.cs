using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class JobDescription
    {
        public int JobDscId { get; set; }
        public string EmpMasterCode { get; set; }
        public string EmpName { get; set; }
        public int DesigId { get; set; }
        public int DeptId { get; set; }
        public DateTime JoiningDate { get; set; }
        public string ReportingTo { get; set; }
        public string JobObjective { get; set; }
        public string DutiesTasks { get; set; }
        public string ImediateSupport { get; set; }
        public string KeyPerformArea { get; set; }
        public bool IsActive { get; set; }
    }
}
