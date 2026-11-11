using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class FoodCharge
    {
        public int FoodId { get; set; }
        public int EmpInfoId { get; set; }
        public decimal FoodCAmount { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string EntryUser { get; set; }
        public DateTime EntryDate { get; set; }
        public string ActionStatus { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
