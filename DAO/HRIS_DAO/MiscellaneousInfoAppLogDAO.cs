using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class MiscellaneousInfoAppLogDAO
    {
        public int Meeting_MiscellaneousInfoAppLogId { get; set; }

        public int? MiscellaneousInfoId { get; set; }

        public int? PreEmpInfoId { get; set; }

        public int? ForEmpInfoId { get; set; }

        public int? Version { get; set; }

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public string ActionStatus { get; set; }

        public string Comments { get; set; }
    }
}
