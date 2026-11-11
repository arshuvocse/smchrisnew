using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public  class JdMaster
    {

        public int JdMasterId { get; set; }

        public int EmpInfoId { get; set; }
        public int FinancialYearId { get; set; }

        public string JdSummary { get; set; }
       public string ActionStatus { get; set; }

        public DateTime? EntryDate { get; set; }

        public string EntryBy { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool? IsDelete { get; set; }

        public DateTime? DeleteDate { get; set; }

        public string DeleteBy { get; set; }
    }
}
