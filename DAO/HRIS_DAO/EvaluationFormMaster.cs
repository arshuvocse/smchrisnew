using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class EvaluationFormMaster
    {

        public int EvaluationFormMasterId { get; set; }

        public int TraingMasreId { get; set; }
        public int TraingingHeadingId { get; set; }
        public int TrainingTopicId { get; set; }

        public string EvaluationFormNo { get; set; }

        public string EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
