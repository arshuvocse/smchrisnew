using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class ProbationEvaluationDetailsDAO
    {
        public int ProbationEvaluationDetailsId { get; set; }
        public int ProbationEvaluationMasterId { get; set; }
        public int TrainingEvaluationMasterId { get; set; }
        public int ValueField { get; set; }
        public string KeyRatingCri { get; set; }
        public bool IsExcellent { get; set; }
        public bool IsGood { get; set; }

        public bool IsSatisfactory { get; set; }
        public bool IsNotSatisfactory { get; set; }

        public string ActionStatus { get; set; }



        public bool IsNochange { get; set; }
        public bool IsMinorChange { get; set; }
        public bool IsReasonableChange { get; set; }
        public bool IsSignificantChange { get; set; }
        public bool IsNotapplicable { get; set; }



        public int EmpInfoId { get; set; }

        public int ContractualEmpManageId { get; set; }






    }
}
