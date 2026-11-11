using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public class TrainingRequisitionMaster
    {

        public int TrainingRequisitionMasterId { get; set; }

        public string TrainingTitle { get; set; }

        public int CompanyId { get; set; }

        public int FinancialYearId { get; set; }

        public int ReqBy { get; set; }

        public string EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsDelete { get; set; }

        public string DeleteBy { get; set; }
        public string Quater { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
