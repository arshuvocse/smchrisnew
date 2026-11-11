using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class JDAppLogDAO
    {
        public int JDAppLogId { get; set; }
        public int JdMasterId { get; set; }
        public int PreEmpInfoId { get; set; }
        public int ForEmpInfoId { get; set; }
        public int Version { get; set; }
        public string ApproveBy { get; set; }
        public DateTime ApproveDate { get; set; }
        public string ActionStatus { get; set; }
        public string Comments { get; set; }

        public int CommentsEMP { get; set; }

    }
}
