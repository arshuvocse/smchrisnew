using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO.ACC_DAO
{
    public class FinancialYearEntry
    {
        public int FinancialYearId { get; set; }
        public string FinancialCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CompanyId { get; set; }
        public string Status { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? InActiveDate { get; set; }
    }
}
