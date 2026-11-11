using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class SalaryBenefitDetails
    {
        public int SBDId { get; set; }
        public int SalaryHeadId { get; set; }
        public int BenefitId { get; set; }
        public decimal Amount { get; set; }
        public string SalHeadName { get; set; }
        public string SalaryHeadType { get; set; }
        public bool IsActive { get; set; }
    }
}
