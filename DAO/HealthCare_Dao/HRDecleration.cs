using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HealthCare_Dao
{
   public class HRDecleration
    {

        public int HRDeclerationId { get; set; }

        public int? CompanyId { get; set; }
        public int? FinancialId { get; set; }

        public decimal? OPD { get; set; }

        public decimal? IPD { get; set; }

        public int? EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

    }
}
