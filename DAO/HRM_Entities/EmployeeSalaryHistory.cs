using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
   public class EmployeeSalaryHistory
    {
       public int SalaryHistoryId { get; set; }
       public int SalaryInfoId { get; set; }
       public int EmpInfoId { get; set; }
       public string SalHeadName { get; set; }
       public decimal Amount { get; set; }
       public string SalHeadType { get; set; }
       public decimal TotalSalary { get; set; }
       public DateTime ActiveDate { get; set; }
       public DateTime InactiveDate { get; set; }
       public string Status { get; set; }
       public int EntryUser { get; set; }
       public DateTime EntryDate { get; set; }
       public bool Modified { get; set; }
       public bool IsActive { get; set; }
    }
}
