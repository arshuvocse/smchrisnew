using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class Probation
    {
        public int ProbitionId { get; set; }
        public int EmpInfoId { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? ProbationPeriodTo { get; set; }
        public string Action { get; set; }
    }
}
