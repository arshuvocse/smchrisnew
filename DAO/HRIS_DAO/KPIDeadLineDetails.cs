using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class KPIDeadLineDetails
    {
        public int KPIDeadLineDetailsId { get; set; }

        public int KPIDeadLineMasterId { get; set; }

        public int EmpinfoId { get; set; }

        public DateTime? DeadLine { get; set; }

        public string Remarks { get; set; }
        public string OptionInfo { get; set; }

        public DateTime? ExtensionDate { get; set; }
    }
}
