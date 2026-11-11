using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public  class LeaveAvail
    {
        public int LeaveAvailId { get; set; }
        public int EmpInfoId { get; set; }
        public string EmpMasterCode { get; set; }
        public string EmpName { get; set; }
        public int LeaveInventoryId { get; set; }
        public string LeaveName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string AvailLeaveQty { get; set; }
        public string LeaveReason { get; set; }
        public int ApprovedLeaveQty { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string Status { get; set; }
        public string ActionStatus { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsActive { get; set; }
    }
}
