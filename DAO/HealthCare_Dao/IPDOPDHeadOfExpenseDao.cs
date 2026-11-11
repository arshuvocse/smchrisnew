using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HealthCare_Dao
{
   public class IPDOPDHeadOfExpenseDao
    {
        public int OIPDHeadOfExpenseId { get; set; }
        public string HeadOfExpense { get; set; }
        public decimal? Amount { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsMaternity { get; set; }
        public string Remaks { get; set; }
        public bool? IsOPD { get; set; }
        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public int CompanyId { get; set; }
        public int HeadOfExpenseMasterId { get; set; }
    }
}
