using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class LeaveInventory
    {
        public int LeaveInventoryId { get; set; }
        public int LeaveId { get; set; }
        public string LeaveCode { get; set; }
        public string LeaveName { get; set; }
        public string DayQty { get; set; }
        public Decimal DayQtyMain { get; set; }
        public string LeaveYear { get; set; }
        public string EmpMasterCode { get; set; }
        public string EmpName { get; set; }
        public int EmpInfoId { get; set; }
        public decimal YearDayQty { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsActive { get; set; }
        public string  DeptName { get; set; }
        public string SectionName { get; set; }
        public string DesigName { get; set; }
        public string UnitName { get; set; }
    }
}
