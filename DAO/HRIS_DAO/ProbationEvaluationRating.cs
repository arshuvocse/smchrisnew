using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class ProbationEvaluationRating
    {
        public int tblProbationEvaluationRatingId { get; set; }
        public string ValueField { get; set; }
        public string TextField { get; set; }
        public bool IsActive { get; set; }
        public int CompanyId { get; set; }
    }
}
