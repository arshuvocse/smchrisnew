using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class ProbationEvaluationMasterDAO
    {
        public int ProbationEvaluationMasterId { get; set; }
        public int TrainingRecordMasterId { get; set; }
        public int EmpInfoId { get; set; }
        public string SupervisorObserv { get; set; }
        public string DeptHeadObserv { get; set; }
        public string DivHeadOvserv { get; set; }
        public bool ExProbation { get; set; }
        public bool ProbationEnd { get; set; }
        public DateTime? ExProDate { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string ActionStatus { get; set; }
        public string ProbationEndReason     { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public DateTime? SeparationDate { get; set; }
        public bool? AutoProcess { get; set; }

    }
}
