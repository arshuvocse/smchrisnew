using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class EvaluationFormDetails
    {

        public int EvaluationFormDetailsId { get; set; }

        public int? EvaluationFormMasterId { get; set; }
        public int TrainingTopicId { get; set; }
        public int TraingingHeadingId { get; set; }

        public string TopicText { get; set; }

        public string FailedText { get; set; }

        public string AverageText { get; set; }

        public string AboveAverageText { get; set; }

        public string ExcellentText { get; set; }
        public bool IsActive { get; set; }

    }
}
