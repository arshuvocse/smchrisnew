using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class EvaluatTrainingDetails
    {

        public int EvaluateTrainingDetailsId { get; set; }

        public int? EvaluateTrainingMaster { get; set; }
        public int EvaluationFormDetailsId { get; set; }

        public int TopicId { get; set; }

        public bool? IsFailed { get; set; }

        public bool IsAverage { get; set; }

        public bool IsAbvAverage { get; set; }

        public bool IsExcellent { get; set; }
    }
}
