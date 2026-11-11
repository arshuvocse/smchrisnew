using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class PFStartStop
    {
        public int PFSSId { get; set; }
        public int EmpInfoId { get; set; }
        public DateTime? ActiveInactiveDate { get; set; }
        public DateTime ActionDate { get; set; }
        public string Action { get; set; }
        public string ActionStatus { get; set; }
        public string EntryUser { get; set; }
        public DateTime EntryDate { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
