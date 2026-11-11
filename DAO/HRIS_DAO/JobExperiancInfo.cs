using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Library.DAO.HRM_Entities
{
   public class JobExperiancInfo
    {
        public int JobExpId { get; set; }
        public int EmpInfoId { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Duration { get; set; }
        
    }
}
