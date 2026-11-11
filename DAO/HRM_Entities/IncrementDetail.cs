using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class IncrementDetail
    {
        public int IncrementDetailId { get; set; }
        public int SalaryHeadId { get; set; }
        public string SalHeadName { get; set; }
        public decimal Amount { get; set; }
        public string SalHeadType { get; set; }
        public DateTime ActiveDate { get; set; }
        public int IncrementId { get; set; }
        public bool IsActive { get; set; }
        
    }
}
