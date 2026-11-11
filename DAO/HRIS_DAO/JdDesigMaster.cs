using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public  class JdDesigMaster
    {

        public int JdDesigMasterId { get; set; }

        public int Designationid { get; set; }
        public int CompanyId { get; set; }
        public int ReportToId { get; set; }
        public int DirectSuperId { get; set; }
        public int InterContId { get; set; }
        public int ExterContId { get; set; }
        public int FinancialYearId { get; set; }
        public int DivisionId { get; set; }
        public int JobLocationId { get; set; }

        public string JdSummary { get; set; }
        public string Education { get; set; }
        public string RelExp { get; set; }
        public string SpecialSkill  { get; set; }
        public string OtherReq { get; set; }
        public string CompSkill { get; set; }
        public DateTime? EntryDate { get; set; }

        public string EntryBy { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool? IsDelete { get; set; }

        public DateTime? DeleteDate { get; set; }

        public string DeleteBy { get; set; }
    }
}
