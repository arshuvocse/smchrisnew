using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class EmployeeSalary
    {
        public int SalaryInfoId { get; set; }
        public int EmpInfoId { get; set; }
        public int SalaryHeadId { get; set; }
        public string SalaryHead { get; set; }
        public decimal Amount { get; set; }
        public string SalHeadType { get; set; }
        public DateTime ActiveDate { get; set; }
        public DateTime InactiveDate { get; set; }
        public string ActionStatus { get; set; }
        public string EntryUser { get; set; }
        public DateTime EntryDate { get; set; }
        public bool Modified { get; set; }
        public int SalaryHistoryId { get; set; }
        public string ApprovedUser { get; set; }
        public DateTime ApprovedDate { get; set; }
        public bool IsActive { get; set; }
      //  public string SalHeadName { get; set; }

    }
}
