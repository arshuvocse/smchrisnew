using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Library.DAO.HRM_Entities
{
   public class EmpTrainingInfo
    {
        public int TrainingId { get; set; }
        public int EmpInfoId { get; set; }
        public string TrainingName { get; set; }
        public string InstituteName { get; set; }
        public string Subject { get; set; }
        public string Duration { get; set; }
        public string Result { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
       public string  Country { get; set; }
        
    }
}
