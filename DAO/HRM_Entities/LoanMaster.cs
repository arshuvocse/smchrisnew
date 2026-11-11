using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class LoanMaster
    {
        public int LoanId { get; set; }
        public int EmpInfoId { get; set; }
        public int DesigId { get; set; }
        public int DeptId { get; set; }
        public decimal LoanAmount { get; set; }
        public int TotalInstallment { get; set; }
        public DateTime SanctionDate { get; set; }
        public DateTime DeductionStartDate { get; set; }
        public decimal InstallAmount { get; set; }
        public string Status { get; set; }
        public string ActionStatus { get; set; }
        public string EntryUser { get; set; }
        public DateTime EntryDate { get; set; }
        public string LoanType { get; set; }
        public bool IsActive { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        
    }
}
