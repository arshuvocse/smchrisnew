using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class SalaryScale
    {
        public int SalScaleId { get; set; }
        public string SalScaleCode { get; set; }
        public string SalScaleName { get; set; }
        public decimal HouseRent { get; set; }
        public decimal Medical { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal IncrementRate { get; set; }
        public string ProvidentFund { get; set; }
        public decimal Conveyance { get; set; }
        public decimal Foodallowance { get; set; }
        public DateTime ActiveDate { get; set; }
        public string ActionStatus { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public decimal Gross { get; set; }
        public bool IsActive { get; set; }
    }
}
