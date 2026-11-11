using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public  class EvaluateTrainingMaster
    {
        public int EvaluateTrainingMasterId { get; set; }

        public int EvaluationFormMasterId { get; set; }
        public int TrainingRecordMasterId { get; set; }
       public int EmpInfoId { get; set; }
       public int? ReportingEmpId { get; set; }


       public string EntryBy { get; set; }
       public string Comments { get; set; }

        public DateTime? EntryDate { get; set; }
    }
}
