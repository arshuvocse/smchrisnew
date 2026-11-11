using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public class AppraisalTrainingNeeds
    {

        public int AppraisalTrainingId { get; set; }

        public int? AppraisalMasterId { get; set; }

        public string TrainingNeeds { get; set; }

        public DateTime? TrainingStart { get; set; }

        public DateTime? TrainingEnd { get; set; }
        public int? Quater { get; set; }
    }
}
