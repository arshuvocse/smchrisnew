using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class LoanDetail
    {
        public int LoanDetailId { get; set; }
        public int LoanId { get; set; }
        public decimal Amount { get; set; }
        public DateTime InstallmentDate { get; set; }
        
        
    }
}
