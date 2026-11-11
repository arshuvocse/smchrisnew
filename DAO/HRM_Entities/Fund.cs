using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class Fund
    {
        public int FundId { get; set; }
        public int EmpInfoId { get; set; }
        public int DesigId { get; set; }
        public int DeptId { get; set; }
        public string FundName { get; set; }
        public decimal Balance { get; set; }
        public DateTime ActiveDate { get; set; }
        public DateTime InactiveDate { get; set; }
        public string Status { get; set; }
        public string EntryUser { get; set; }
        public DateTime EntryDate { get; set; }

        public int CompanyInfoId { get; set; }
        public int UnitId { get; set; }
        public int DivisionId { get; set; }
        public int SectionId { get; set; }
        
    }
}
