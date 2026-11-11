using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HealthCare_Dao
{
    public class HeadOfExpenseMaster
    {
        public int HeadOfExpenseMasterId { get; set; }

        public int? CompanyId { get; set; }

        public bool? IsOPD { get; set; }

        public int? EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
