using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class RestLeave
    {
        public int RestLeaveId { get; set; }
        public int EmpInfoId { get; set; }
        public int DeptId { get; set; }
        public DateTime OnRDate { get; set; }
        public string Purpose { get; set; }
        public string ActionStatus { get; set; }
        public string ActionRemarks { get; set; }
        public string ApprovedUser { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string EntryUser { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsActive { get; set; }
        public string DeleteBy { get; set; }
        public string DeleteDate { get; set; }
    }
}
