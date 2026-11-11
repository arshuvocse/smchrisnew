using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class YearlyPFBenefit
    {
        public int PFBenefitId { get; set; }
        public int EmpInfoId { get; set; }
        public int FianacialYear { get; set; }
        public decimal IntPer { get; set; }
        public decimal EmpAmount { get; set; }
        public decimal CompAmount { get; set; }
        public decimal InterAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string ActionStatus { get; set; }
        public string EntryUser { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal ArearAmount { get; set; }
        public string Purpose { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
