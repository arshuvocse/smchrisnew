using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class OnDuty
    {
        public int OnDutyId { get; set; }
        public int EmpInfoId { get; set; }
        public DateTime OnDDate { get; set; }
        public DateTime OnTDate { get; set; }
        public string DutyLocation { get; set; }
        public string Purpose { get; set; }
        public string ActionStatus { get; set; }
        public string ActionRemarks { get; set; }
        public string ApprovedUser { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string EntryUser { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsActive { get; set; }
    }
}
