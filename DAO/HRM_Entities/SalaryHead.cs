using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class SalaryHead
    {
        public int SalaryHeadId { get; set; }
        public string SalaryHeadCode { get; set; }
        public string SalaryHeadName { get; set; }
        public int SHeadTypeId { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
